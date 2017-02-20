using UnityEngine;
using System.Collections.Generic;

public abstract class EgoConstraint
{
	protected BitMask _mask = new BitMask( ComponentIDs.GetCount() );
	public EgoSystem system;

    public EgoParentConstraint parentConstraint = null;
    public EgoConstraint childConstraint = null;

    public EgoComponent currentEgoComponent;

    public Dictionary<EgoComponent, EgoBundle> rootBundles = new Dictionary<EgoComponent, EgoBundle>();
    public Dictionary<EgoComponent, Dictionary< EgoComponent, EgoBundle>> childBundles = new Dictionary<EgoComponent, Dictionary<EgoComponent, EgoBundle>>();

	public void SetSystem( EgoSystem system )
	{
		this.system = system;
		if( childConstraint != null ){ childConstraint.SetSystem( system ); }
	}

    protected bool CanUpdate( EgoComponent egoComponent )
    {
		var comparisonMask = new BitMask( egoComponent.mask ).And( _mask );
		return comparisonMask == _mask;
    }

	/// <summary>
	/// Create a Bundle for the given egoComponent outright
	/// Assumes the EgoComponent has the required components
	/// </summary>
	/// <param name="egoComponent"></param>
	protected abstract EgoBundle CreateBundle( EgoComponent egoComponent );

    /// <summary>
    /// Try to create Bundles for the given EgoComponent, and all of its children (recursively)
    /// </summary>
    /// <param name="egoComponent"></param>
    public void CreateBundles( EgoComponent egoComponent )
    {
		if( egoComponent == null ) { return; }

		// Only Create Bundles from the youngest EgoConstraint
		if( childConstraint != null )
		{
			childConstraint.CreateBundles( egoComponent );
		}
		else
		{
			// Recurse to All Children EgoComponents
			{
				var egoTransform = egoComponent.transform;
				var childCount = egoComponent.transform.childCount;
				for( var i = 0; i < childCount; i++ )
				{
					CreateBundles( egoTransform.GetChild( i ).GetComponent<EgoComponent>() );
				}
			}

			// Setup EgoConstraint & EgoComponent Ancestries
			// Early exit if the given EgoComponent and ancestors can't satisfy all constraints
			var tuples = new List<Tuple<EgoConstraint, EgoComponent>>();
			{
				var currentConstraint = this;
				var currentEgoComponent = egoComponent;

				while( currentConstraint != null )
				{
					if( currentEgoComponent == null || !currentConstraint.CanUpdate( currentEgoComponent ) ) { return; }

					tuples.Add( new Tuple<EgoConstraint, EgoComponent>( currentConstraint, currentEgoComponent ) );
					currentConstraint = currentConstraint.parentConstraint;
					currentEgoComponent = currentEgoComponent.parent;
				}
			}

			var endIndex = tuples.Count - 1;
			var topConstraint = tuples[ endIndex ].first;
			var topEgoComponent = tuples[ endIndex ].second;

			topConstraint.rootBundles[ topEgoComponent ] = topConstraint.CreateBundle( topEgoComponent );

			for( var i = endIndex; i > 0; i-- )
			{
				var currentChildConstraint = tuples[ i - 1 ].first;
				var currentChildEgoComponent = tuples[ i - 1 ].second;
				var currentParentConstraint = tuples[ i ].first;
				var currentParentEgoComponent = tuples[ i ].second;

				if( !currentParentConstraint.childBundles.ContainsKey( currentParentEgoComponent ) )
				{
					currentParentConstraint.childBundles[ currentParentEgoComponent ] = new Dictionary<EgoComponent, EgoBundle>();
				}

				currentParentConstraint.childBundles[ currentParentEgoComponent ][ currentChildEgoComponent ] = currentChildConstraint.CreateBundle( currentChildEgoComponent );
			}
		}
    }

	public void RemoveBundles( EgoComponent egoComponent )
	{
		RemoveChildBundles( this, egoComponent );
		RemoveParentBundles( this, egoComponent );
		rootBundles.Remove( egoComponent );
	}

	public void SetParent( EgoComponent newParent, EgoComponent child, bool worldPositionStays )
	{
		if( child.parent == newParent ) { return; }

		var currentParent = child.parent;

        if( currentParent != null )
		{
            RemoveBundles( currentParent );
        }
		
		if( newParent != null )
		{
			child.transform.SetParent( newParent.transform, worldPositionStays );
		}
		else
		{
			child.transform.SetParent( null, worldPositionStays );
		}

		CreateBundles( child );
	}
	
	void RemoveChildBundles( EgoConstraint constraint, EgoComponent egoComponent )
	{
		if( constraint.childBundles.ContainsKey( egoComponent ) )
		{
			if( constraint.childConstraint != null )
			{
				var lookup = constraint.childBundles[ egoComponent ];
				foreach( var childEgoComponent in lookup.Keys )
				{
					RemoveChildBundles( constraint.childConstraint, childEgoComponent );
				}
				lookup.Clear();
			}
			else
			{
				constraint.childBundles.Remove( egoComponent );
			}
		}
	}

	void RemoveParentBundles( EgoConstraint childConstraint, EgoComponent childEgoComponent )
	{
		var parentConstraint = childConstraint.parentConstraint;
		var parentEgoComponent = childEgoComponent.parent;
		
		if( parentConstraint != null && parentEgoComponent != null && parentConstraint.childBundles.ContainsKey( parentEgoComponent ) )
		{
			parentConstraint.childBundles[ parentEgoComponent ].Remove( childEgoComponent );
			if( parentConstraint.childBundles[ parentEgoComponent ].Count <= 0 )
			{
				parentConstraint.childBundles.Remove( parentEgoComponent );
			}

			RemoveParentBundles( parentConstraint, parentEgoComponent );
		}
	}

    /// <summary>
    /// Get the lookup based on the parent constraint's current bundle (if applicable)
    /// </summary>
    /// <typeparam name="B"></typeparam>
    /// <param name="defaultLookup"></param>
    public Dictionary<EgoComponent, B> GetLookup<B>( Dictionary<EgoComponent, B> defaultLookup ) 
        where B : EgoBundle
    {
        if( parentConstraint != null && parentConstraint.currentEgoComponent != null )
        {
            return parentConstraint.childBundles[ parentConstraint.currentEgoComponent ] as Dictionary<EgoComponent, B>;
        }
        else
        {
            return defaultLookup;
        }
    }
}
