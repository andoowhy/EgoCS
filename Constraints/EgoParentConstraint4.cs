namespace EgoCS
{
    using System.Collections;
    using UnityEngine;
    using System.Collections.Generic;

    public class EgoParentConstraint< C1, C2, C3, C4, CS1 > : EgoParentConstraint, IEnumerable< (EgoComponent, C1, C2, C3, C4, CS1) >
        where C1 : Component
        where C2 : Component
        where C3 : Component
        where C4 : Component
        where CS1 : EgoConstraint, new()
    {
        public EgoParentConstraint()
        {
            childConstraint = new CS1();
            childConstraint.parentConstraint = this;

            _mask[ ComponentUtils.Get< C1 >() ] = true;
            _mask[ ComponentUtils.Get< C2 >() ] = true;
            _mask[ ComponentUtils.Get< C3 >() ] = true;
            _mask[ ComponentUtils.Get< C4 >() ] = true;
            _mask[ ComponentUtils.Get< EgoComponent >() ] = true;
        }

        protected override EgoBundle CreateBundle( EgoComponent egoComponent )
        {
            return new EgoBundle< C1, C2, C3, C4 >(
                egoComponent.GetComponent< C1 >(),
                egoComponent.GetComponent< C2 >(),
                egoComponent.GetComponent< C3 >(),
                egoComponent.GetComponent< C4 >()
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

            egoCS.AddAddedComponentCallback( typeof( C4 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( C4 ), CreateBundles );

            egoCS.AddSetParentCallback( SetParent );
        }

        IEnumerator< (EgoComponent, C1, C2, C3, C4, CS1) > IEnumerable< (EgoComponent, C1, C2, C3, C4, CS1) >.GetEnumerator()
        {
            var lookup = GetLookup( rootBundles );
            foreach( var kvp in lookup )
            {
                currentEgoComponent = kvp.Key;
                var bundle = kvp.Value as EgoBundle< C1, C2, C3, C4 >;
                yield return ( currentEgoComponent, bundle.component1, bundle.component2, bundle.component3, bundle.component4, childConstraint as CS1 );
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return this;
        }
    }
}