using UnityEngine;
using System.Collections.Generic;

public abstract class EgoConstraint
{
    public EgoParentConstraint parentConstraint = null;
    public EgoConstraint childConstraint = null;

    public EgoConstraint[] ancestorConstraints
    {
        get
        {
            if( _ancestorConstraints == null )
            {
                var list = new List<EgoConstraint>();
                list.Add( this );

                var currentChildConstraint = childConstraint;
                while( currentChildConstraint != null )
                {
                    list.Add( currentChildConstraint );
                    currentChildConstraint = currentChildConstraint.childConstraint;
                }
                list.Reverse();

                _ancestorConstraints = list.ToArray();
            }
            return _ancestorConstraints;
        }
    }

    public EgoComponent currentEgoComponent;
    public Dictionary<EgoComponent, EgoBundle> rootBundles = new Dictionary<EgoComponent, EgoBundle>();
    public Dictionary<EgoComponent, Dictionary< EgoComponent, EgoBundle>> childBundles = new Dictionary<EgoComponent, Dictionary<EgoComponent, EgoBundle>>();

    protected EgoConstraint[] _ancestorConstraints;
    protected BitMask _mask = new BitMask( ComponentIDs.GetCount() );

    protected bool CanUpdate( EgoComponent egoComponent )
    {
        return Ego.CanUpdate( _mask, egoComponent.mask );
    }

    /// <summary>
    /// Try to create Bundles for the given EgoComponent, and all of its children (recursively)
    /// </summary>
    /// <param name="egoComponent"></param>
    public void CreateBundles( EgoComponent egoComponent )
    {
        #region Recurse to Children
        {
            var egoTransform = egoComponent.transform;
            var childCount = egoComponent.transform.childCount;
            for( var i = 0; i < childCount; i++ )
            {
                CreateBundles( egoTransform.GetChild( i ).GetComponent<EgoComponent>() );
            }
        }
        #endregion

        #region Link Up EgoComponents & Constraint Hierarchy
        var egoComponents = new List<EgoComponent>();
        {
            var currentEgoComponent = egoComponent;
            for( var i = 0; i < ancestorConstraints.Length; i++ )
            {
                if( currentEgoComponent == null || !ancestorConstraints[i].CanUpdate( egoComponent ) ) { break; }
                egoComponents.Add( currentEgoComponent );
                currentEgoComponent = currentEgoComponent.parent;
            }
        }
        #endregion

        #region Create Bundles
        // TODO: flip for loop order
        var endIndex = ancestorConstraints.Length - 1;
        if( ancestorConstraints.Length == egoComponents.Count && endIndex > 0 )
        {
            var topConstraint = ancestorConstraints[ endIndex ];
            var topEgoComponent = egoComponents[ endIndex ];

            topConstraint.rootBundles[ topEgoComponent ] = topConstraint.CreateBundle( topEgoComponent );
            
            for( var i = endIndex; i > 0; i-- )
            {
                var parentConstraint = ancestorConstraints[ i ];
                var parentEgoComponent = egoComponents[ i ];

                var childConstraint = ancestorConstraints[ i - 1 ];
                var childEgoComponent = egoComponents[ i - 1 ];

                if( !parentConstraint.childBundles.ContainsKey( parentEgoComponent ) )
                {
                    parentConstraint.childBundles[parentEgoComponent] = new Dictionary<EgoComponent, EgoBundle>();
                }

                parentConstraint.childBundles[ parentEgoComponent ][ childEgoComponent ] = childConstraint.CreateBundle( childEgoComponent );
            }
        }
        #endregion
    }

    /// <summary>
    /// Create a Bundle for the given egoComponent outright
    /// Assumes the EgoComponent has the required components
    /// </summary>
    /// <param name="egoComponent"></param>
    protected abstract EgoBundle CreateBundle( EgoComponent egoComponent );

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
