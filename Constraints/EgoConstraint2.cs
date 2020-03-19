using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class EgoConstraint< C1, C2 > : EgoConstraint, IEnumerable< (EgoComponent, C1, C2) >
    where C1 : Component
    where C2 : Component
{
    public EgoConstraint()
    {
        _mask[ ComponentUtils.Get< C1 >() ] = true;
        _mask[ ComponentUtils.Get< C2 >() ] = true;
        _mask[ ComponentUtils.Get< EgoComponent >() ] = true;
    }

    protected override EgoBundle CreateBundle( EgoComponent egoComponent )
    {
        return new EgoBundle< C1, C2 >(
            egoComponent.GetComponent< C1 >(),
            egoComponent.GetComponent< C2 >()
        );
    }

    public override void CreateConstraintCallbacks( EgoInterface egoInterface )
    {
        egoInterface.AddAddedComponentCallback( typeof( C1 ), CreateBundles );
        egoInterface.AddDestroyedComponentCallback( typeof( C1 ), CreateBundles );

        egoInterface.AddAddedComponentCallback( typeof( C2 ), CreateBundles );
        egoInterface.AddDestroyedComponentCallback( typeof( C2 ), CreateBundles );
    }

    IEnumerator< (EgoComponent, C1, C2) > IEnumerable< (EgoComponent, C1, C2) >.GetEnumerator()
    {
        var lookup = GetLookup( rootBundles );
        foreach( var kvp in lookup )
        {
            currentEgoComponent = kvp.Key;
            var bundle = kvp.Value as EgoBundle< C1, C2 >;
            yield return ( currentEgoComponent, bundle.component1, bundle.component2 );
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        yield return this;
    }
}