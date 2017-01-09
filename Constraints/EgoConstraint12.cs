using UnityEngine;
using System.Collections.Generic;
using System;

public class EgoConstraint<C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12> : EgoConstraint
	where C1 : Component
	where C2 : Component
	where C3 : Component
	where C4 : Component
	where C5 : Component
	where C6 : Component
	where C7 : Component
	where C8 : Component
	where C9 : Component
	where C10 : Component
	where C11 : Component
	where C12 : Component
{
    public EgoConstraint()
    {
		_mask[ ComponentIDs.Get( typeof( C1 ) ) ] = true;
		_mask[ ComponentIDs.Get( typeof( C2 ) ) ] = true;
		_mask[ ComponentIDs.Get( typeof( C3 ) ) ] = true;
		_mask[ ComponentIDs.Get( typeof( C4 ) ) ] = true;
		_mask[ ComponentIDs.Get( typeof( C5 ) ) ] = true;
		_mask[ ComponentIDs.Get( typeof( C6 ) ) ] = true;
		_mask[ ComponentIDs.Get( typeof( C7 ) ) ] = true;
		_mask[ ComponentIDs.Get( typeof( C8 ) ) ] = true;
		_mask[ ComponentIDs.Get( typeof( C9 ) ) ] = true;
		_mask[ ComponentIDs.Get( typeof( C10 ) ) ] = true;
		_mask[ ComponentIDs.Get( typeof( C11 ) ) ] = true;
		_mask[ ComponentIDs.Get( typeof( C12 ) ) ] = true;
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

		EgoEvents<AddedComponent<C10>>.AddHandler( e => CreateBundles( e.egoComponent ) );
		EgoEvents<DestroyedComponent<C10>>.AddHandler( e => RemoveBundles( e.egoComponent ) );

		EgoEvents<AddedComponent<C11>>.AddHandler( e => CreateBundles( e.egoComponent ) );
		EgoEvents<DestroyedComponent<C11>>.AddHandler( e => RemoveBundles( e.egoComponent ) );

		EgoEvents<AddedComponent<C12>>.AddHandler( e => CreateBundles( e.egoComponent ) );
		EgoEvents<DestroyedComponent<C12>>.AddHandler( e => RemoveBundles( e.egoComponent ) );
    }

	protected override EgoBundle CreateBundle( EgoComponent egoComponent )
	{
		return new EgoBundle<C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12>(
			egoComponent.GetComponent<C1>(),
			egoComponent.GetComponent<C2>(),
			egoComponent.GetComponent<C3>(),
			egoComponent.GetComponent<C4>(),
			egoComponent.GetComponent<C5>(),
			egoComponent.GetComponent<C6>(),
			egoComponent.GetComponent<C7>(),
			egoComponent.GetComponent<C8>(),
			egoComponent.GetComponent<C9>(),
			egoComponent.GetComponent<C10>(),
			egoComponent.GetComponent<C11>(),
			egoComponent.GetComponent<C12>()
		);
	}

	public delegate void ForEachGameObjectDelegate(
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
		C10 component10,
		C11 component11,
		C12 component12
	);

	public void ForEachGameObject( ForEachGameObjectDelegate callback )
	{
		var lookup = GetLookup( rootBundles );
		foreach( var kvp in lookup )
		{
			currentEgoComponent = kvp.Key;
			var bundle = kvp.Value as EgoBundle<C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12>;
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
				bundle.component10,
				bundle.component11,
				bundle.component12
			);
		}
	}
}
