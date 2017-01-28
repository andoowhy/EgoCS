using UnityEngine;
using System.Collections.Generic;

public class EgoParentConstraint<C1, C2, C3, C4, C5, C6, C7, C8, C9, CS1> : EgoParentConstraint
	where C1 : Component
	where C2 : Component
	where C3 : Component
	where C4 : Component
	where C5 : Component
	where C6 : Component
	where C7 : Component
	where C8 : Component
	where C9 : Component
    where CS1 : EgoConstraint, new()
{
    public EgoParentConstraint()
    {
        childConstraint = new CS1();
        childConstraint.parentConstraint = this;

		_mask[ ComponentIDs.Get( typeof( C1 ) ) ] = true;
		_mask[ ComponentIDs.Get( typeof( C2 ) ) ] = true;
		_mask[ ComponentIDs.Get( typeof( C3 ) ) ] = true;
		_mask[ ComponentIDs.Get( typeof( C4 ) ) ] = true;
		_mask[ ComponentIDs.Get( typeof( C5 ) ) ] = true;
		_mask[ ComponentIDs.Get( typeof( C6 ) ) ] = true;
		_mask[ ComponentIDs.Get( typeof( C7 ) ) ] = true;
		_mask[ ComponentIDs.Get( typeof( C8 ) ) ] = true;
		_mask[ ComponentIDs.Get( typeof( C9 ) ) ] = true;
        _mask[ComponentIDs.Get( typeof( EgoComponent ) )] = true;

		EgoEvents<AddedComponent<C1>>.AddHandler( e => CreateBundles( e.egoComponent ) );
		EgoEvents<DestroyedComponent<C1>>.AddHandler( e => RemoveBundles( e.egoComponent ) );

		EgoEvents<AddedComponent<C2>>.AddHandler( e => CreateBundles( e.egoComponent ) );
		EgoEvents<DestroyedComponent<C2>>.AddHandler( e => RemoveBundles( e.egoComponent ) );

		EgoEvents<AddedComponent<C3>>.AddHandler( e => CreateBundles( e.egoComponent ) );
		EgoEvents<DestroyedComponent<C3>>.AddHandler( e => RemoveBundles( e.egoComponent ) );

		EgoEvents<AddedComponent<C4>>.AddHandler( e => CreateBundles( e.egoComponent ) );
		EgoEvents<DestroyedComponent<C4>>.AddHandler( e => RemoveBundles( e.egoComponent ) );

		EgoEvents<AddedComponent<C5>>.AddHandler( e => CreateBundles( e.egoComponent ) );
		EgoEvents<DestroyedComponent<C5>>.AddHandler( e => RemoveBundles( e.egoComponent ) );

		EgoEvents<AddedComponent<C6>>.AddHandler( e => CreateBundles( e.egoComponent ) );
		EgoEvents<DestroyedComponent<C6>>.AddHandler( e => RemoveBundles( e.egoComponent ) );

		EgoEvents<AddedComponent<C7>>.AddHandler( e => CreateBundles( e.egoComponent ) );
		EgoEvents<DestroyedComponent<C7>>.AddHandler( e => RemoveBundles( e.egoComponent ) );

		EgoEvents<AddedComponent<C8>>.AddHandler( e => CreateBundles( e.egoComponent ) );
		EgoEvents<DestroyedComponent<C8>>.AddHandler( e => RemoveBundles( e.egoComponent ) );

		EgoEvents<AddedComponent<C9>>.AddHandler( e => CreateBundles( e.egoComponent ) );
		EgoEvents<DestroyedComponent<C9>>.AddHandler( e => RemoveBundles( e.egoComponent ) );

		EgoEvents<SetParent>.AddHandler( e => SetParent( e.parent, e.child, e.worldPositionStays ) );
    }

    protected override EgoBundle CreateBundle( EgoComponent egoComponent )
    {
        return new EgoBundle<C1, C2, C3, C4, C5, C6, C7, C8, C9>(
			egoComponent.GetComponent<C1>(),
			egoComponent.GetComponent<C2>(),
			egoComponent.GetComponent<C3>(),
			egoComponent.GetComponent<C4>(),
			egoComponent.GetComponent<C5>(),
			egoComponent.GetComponent<C6>(),
			egoComponent.GetComponent<C7>(),
			egoComponent.GetComponent<C8>(),
			egoComponent.GetComponent<C9>()
		);
    }

	public delegate void ForEachGameObjectWithChildrentDelegate(
		EgoComponent egoComponent,
		C1 component1,
		C2 component2,
		C3 component3,
		C4 component4,
		C5 component5,
		C6 component6,
		C7 component7,
		C8 component8,
		C9 component9,
		CS1 childConstraint
	);

	public void ForEachGameObject( ForEachGameObjectWithChildrentDelegate callback )
	{
		var lookup = GetLookup( rootBundles );
		foreach( var kvp in lookup )
		{
			currentEgoComponent = kvp.Key;
			var bundle = kvp.Value as EgoBundle<C1, C2, C3, C4, C5, C6, C7, C8, C9>;
			callback(
				currentEgoComponent,
				bundle.component1,
				bundle.component2,
				bundle.component3,
				bundle.component4,
				bundle.component5,
				bundle.component6,
				bundle.component7,
				bundle.component8,
				bundle.component9,
				childConstraint as CS1
			);
		}
	}
}
