namespace EgoCS
{
    using System.Collections;
    using UnityEngine;
    using System.Collections.Generic;

    public class ParentConstraint< TComponent1, TComponent2, TComponent3, TComponent4, CS1 > : ParentConstraint, IEnumerable< (EgoComponent, TComponent1, TComponent2, TComponent3, TComponent4, CS1) >
        where TComponent1 : Component
        where TComponent2 : Component
        where TComponent3 : Component
        where TComponent4 : Component
        where CS1 : Constraint, new()
    {
        public ParentConstraint()
        {
            childConstraint = new CS1();
            childConstraint.parentConstraint = this;

            _mask[ ComponentUtils.Get< TComponent1 >() ] = true;
            _mask[ ComponentUtils.Get< TComponent2 >() ] = true;
            _mask[ ComponentUtils.Get< TComponent3 >() ] = true;
            _mask[ ComponentUtils.Get< TComponent4 >() ] = true;
            _mask[ ComponentUtils.Get< EgoComponent >() ] = true;
        }

        protected override Bundle CreateBundle( EgoComponent egoComponent )
        {
            return new Bundle< TComponent1, TComponent2, TComponent3, TComponent4 >(
                egoComponent.GetComponent< TComponent1 >(),
                egoComponent.GetComponent< TComponent2 >(),
                egoComponent.GetComponent< TComponent3 >(),
                egoComponent.GetComponent< TComponent4 >()
            );
        }

        public override void CreateConstraintCallbacks( EgoCS egoCS )
        {
            egoCS.AddAddedComponentCallback( typeof( TComponent1 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( TComponent1 ), CreateBundles );

            egoCS.AddAddedComponentCallback( typeof( TComponent2 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( TComponent2 ), CreateBundles );

            egoCS.AddAddedComponentCallback( typeof( TComponent3 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( TComponent3 ), CreateBundles );

            egoCS.AddAddedComponentCallback( typeof( TComponent4 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( TComponent4 ), CreateBundles );

            egoCS.AddSetParentCallback( SetParent );
        }

        IEnumerator< (EgoComponent, TComponent1, TComponent2, TComponent3, TComponent4, CS1) > IEnumerable< (EgoComponent, TComponent1, TComponent2, TComponent3, TComponent4, CS1) >.GetEnumerator()
        {
            var lookup = GetLookup( rootBundles );
            foreach( var kvp in lookup )
            {
                currentEgoComponent = kvp.Key;
                var bundle = kvp.Value as Bundle< TComponent1, TComponent2, TComponent3, TComponent4 >;
                yield return ( currentEgoComponent, bundle.component1, bundle.component2, bundle.component3, bundle.component4, childConstraint as CS1 );
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return this;
        }
    }
}