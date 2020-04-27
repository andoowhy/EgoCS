using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using NotImplementedException = System.NotImplementedException;

namespace EgoCS
{
    public class Constraint< TComponent1, TComponent2, TComponent3, TComponent4, TComponent5 > : Constraint, IEnumerable< (EgoComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5) >
        where TComponent1 : Component
        where TComponent2 : Component
        where TComponent3 : Component
        where TComponent4 : Component
        where TComponent5 : Component
    {
        public override void InitMask()
        {
            mask[ ComponentUtils.Get<TComponent1>() ] = true;
            mask[ ComponentUtils.Get<TComponent2>() ] = true;
            mask[ ComponentUtils.Get<TComponent3>() ] = true;
            mask[ ComponentUtils.Get<TComponent4>() ] = true;
            mask[ ComponentUtils.Get<TComponent5>() ] = true;
            mask[ ComponentUtils.Get<EgoComponent>() ] = true;
        }

        protected override Bundle CreateBundle( EgoComponent egoComponent )
        {
            return new Bundle< TComponent1, TComponent2, TComponent3, TComponent4, TComponent5 >(
                egoComponent.GetComponent< TComponent1 >(),
                egoComponent.GetComponent< TComponent2 >(),
                egoComponent.GetComponent< TComponent3 >(),
                egoComponent.GetComponent< TComponent4 >(),
                egoComponent.GetComponent< TComponent5 >()
            );
        }

        public override void CreateConstraintCallbacks( EgoCS egoCS )
        {
            egoCS.AddAddedComponentCallback( typeof( TComponent1 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( TComponent1 ), RemoveBundles );

            egoCS.AddAddedComponentCallback( typeof( TComponent2 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( TComponent2 ), RemoveBundles );

            egoCS.AddAddedComponentCallback( typeof( TComponent3 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( TComponent3 ), RemoveBundles );

            egoCS.AddAddedComponentCallback( typeof( TComponent4 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( TComponent4 ), RemoveBundles );

            egoCS.AddAddedComponentCallback( typeof( TComponent5 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( TComponent5 ), RemoveBundles );
        }

        IEnumerator< (EgoComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5) > IEnumerable< (EgoComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5) >.GetEnumerator()
        {
            var lookup = GetLookup( rootBundles );
            foreach( var kvp in lookup )
            {
                currentEgoComponent = kvp.Key;
                var bundle = kvp.Value as Bundle< TComponent1, TComponent2, TComponent3, TComponent4, TComponent5 >;
                yield return ( currentEgoComponent, bundle.component1, bundle.component2, bundle.component3, bundle.component4, bundle.component5 );
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return this;
        }
    }
}