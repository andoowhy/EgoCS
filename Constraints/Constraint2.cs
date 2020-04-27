using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using NotImplementedException = System.NotImplementedException;

namespace EgoCS
{
    public class Constraint< TComponent1, TComponent2 > : Constraint, IEnumerable< (EgoComponent, TComponent1, TComponent2) >
        where TComponent1 : Component
        where TComponent2 : Component
    {
        public override void InitMask()
        {
            mask[ ComponentUtils.Get<TComponent1>() ] = true;
            mask[ ComponentUtils.Get<TComponent2>() ] = true;
            mask[ ComponentUtils.Get<EgoComponent>() ] = true;
        }

        protected override Bundle CreateBundle( EgoComponent egoComponent )
        {
            return new Bundle< TComponent1, TComponent2 >(
                egoComponent.GetComponent< TComponent1 >(),
                egoComponent.GetComponent< TComponent2 >()
            );
        }

        public override void CreateConstraintCallbacks( EgoCS egoCS )
        {
            egoCS.AddAddedComponentCallback( typeof( TComponent1 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( TComponent1 ), RemoveBundles );

            egoCS.AddAddedComponentCallback( typeof( TComponent2 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( TComponent2 ), RemoveBundles );
        }

        IEnumerator< (EgoComponent, TComponent1, TComponent2) > IEnumerable< (EgoComponent, TComponent1, TComponent2) >.GetEnumerator()
        {
            var lookup = GetLookup( rootBundles );
            foreach( var kvp in lookup )
            {
                currentEgoComponent = kvp.Key;
                var bundle = kvp.Value as Bundle< TComponent1, TComponent2 >;
                yield return ( currentEgoComponent, bundle.component1, bundle.component2 );
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return this;
        }
    }
}