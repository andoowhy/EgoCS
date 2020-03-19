using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class EgoConstraint< C1 > : EgoConstraint, IEnumerable< (EgoComponent, C1) >
    where C1 : Component
{
    public EgoConstraint()
    {
        _mask[ ComponentUtils.Get< C1 >() ] = true;
        _mask[ ComponentUtils.Get< EgoComponent >() ] = true;
    }

    protected override EgoBundle CreateBundle( EgoComponent egoComponent )
    {
        return new EgoBundle< C1 >(
            egoComponent.GetComponent< C1 >()
        );
    }

    public override void CreateConstraintCallbacks( EgoInterface egoInterface )
    {
        egoInterface.AddAddedComponentCallback( typeof( C1 ), CreateBundles );
        egoInterface.AddDestroyedComponentCallback( typeof( C1 ), RemoveBundles );
    }

    IEnumerator< (EgoComponent, C1) > IEnumerable< (EgoComponent, C1) >.GetEnumerator()
    {
        var lookup = GetLookup( rootBundles );
        foreach( var kvp in lookup )
        {
            currentEgoComponent = kvp.Key;
            var bundle = kvp.Value as EgoBundle< C1 >;
            yield return ( currentEgoComponent, bundle.component1 );
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        yield return this;
    }
}