using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class EgoConstraint< C1, C2, C3 > : EgoConstraint, IEnumerable< (EgoComponent, C1, C2, C3) >
    where C1 : Component
    where C2 : Component
    where C3 : Component
{
    public EgoConstraint()
    {
        _mask[ ComponentUtils.Get< C1 >() ] = true;
        _mask[ ComponentUtils.Get< C2 >() ] = true;
        _mask[ ComponentUtils.Get< C3 >() ] = true;
        _mask[ ComponentUtils.Get< EgoComponent >() ] = true;
    }

    protected override EgoBundle CreateBundle( EgoComponent egoComponent )
    {
        return new EgoBundle< C1, C2, C3 >(
            egoComponent.GetComponent< C1 >(),
            egoComponent.GetComponent< C2 >(),
            egoComponent.GetComponent< C3 >()
        );
    }

    public override void CreateConstraintCallbacks( EgoCS egoCS )
    {
        egoCS.AddAddedComponentCallback( typeof( C1 ), CreateBundles );
        egoCS.AddDestroyedComponentCallback( typeof( C1 ), CreateBundles );

        egoCS.AddAddedComponentCallback( typeof( C2 ), CreateBundles );
        egoCS.AddDestroyedComponentCallback( typeof( C2 ), CreateBundles );

        egoCS.AddAddedComponentCallback( typeof( C3 ), CreateBundles );
        egoCS.AddDestroyedComponentCallback( typeof( C3 ), CreateBundles );
    }

    IEnumerator< (EgoComponent, C1, C2, C3) > IEnumerable< (EgoComponent, C1, C2, C3) >.GetEnumerator()
    {
        var lookup = GetLookup( rootBundles );
        foreach( var kvp in lookup )
        {
            currentEgoComponent = kvp.Key;
            var bundle = kvp.Value as EgoBundle< C1, C2, C3 >;
            yield return ( currentEgoComponent, bundle.component1, bundle.component2, bundle.component3 );
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        yield return this;
    }
}