using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace EgoCS
{
    public class ParentConstraint< TComponent1, CS1 > : ParentConstraint, IEnumerable< (EgoComponent, TComponent1, CS1) >
        where TComponent1 : Component
        where CS1 : Constraint, new()
    {
        public ParentConstraint()
        {
            childConstraint = new CS1();
            childConstraint.parentConstraint = this;
        }

        public override void InitMask()
        {
            mask[ ComponentUtils.Get<TComponent1>() ] = true;
            mask[ ComponentUtils.Get<EgoComponent>() ] = true;

            childConstraint.InitMask();
        }

        protected override Bundle CreateBundle( EgoComponent egoComponent )
        {
            return new Bundle< TComponent1 >(
                egoComponent.GetComponent< TComponent1 >()
            );
        }

        public override void CreateConstraintCallbacks( EgoCS egoCS )
        {
            egoCS.AddAddedComponentCallback( typeof( TComponent1 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( TComponent1 ), RemoveBundles );

            egoCS.AddSetParentCallback( SetParent );
        }

        IEnumerator< (EgoComponent, TComponent1, CS1) > IEnumerable< (EgoComponent, TComponent1, CS1) >.GetEnumerator()
        {
            var lookup = GetLookup( rootBundles );
            foreach( var kvp in lookup )
            {
                currentEgoComponent = kvp.Key;
                var bundle = kvp.Value as Bundle< TComponent1 >;
                yield return ( currentEgoComponent, bundle.component1, childConstraint as CS1 );
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return this;
        }
    }
}