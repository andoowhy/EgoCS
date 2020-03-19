namespace EgoCS
{
    using System.Collections;
    using UnityEngine;
    using System.Collections.Generic;

    public class EgoParentConstraint< C1, CS1 > : EgoParentConstraint, IEnumerable< (EgoComponent, C1, CS1) >
        where C1 : Component
        where CS1 : EgoConstraint, new()
    {
        public EgoParentConstraint()
        {
            childConstraint = new CS1();
            childConstraint.parentConstraint = this;

            _mask[ ComponentUtils.Get< C1 >() ] = true;
            _mask[ ComponentUtils.Get< EgoComponent >() ] = true;
        }

        protected override EgoBundle CreateBundle( EgoComponent egoComponent )
        {
            return new EgoBundle< C1 >(
                egoComponent.GetComponent< C1 >()
            );
        }

        public override void CreateConstraintCallbacks( EgoCS egoCS )
        {
            egoCS.AddAddedComponentCallback( typeof( C1 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( C1 ), CreateBundles );

            egoCS.AddSetParentCallback( SetParent );
        }

        IEnumerator< (EgoComponent, C1, CS1) > IEnumerable< (EgoComponent, C1, CS1) >.GetEnumerator()
        {
            var lookup = GetLookup( rootBundles );
            foreach( var kvp in lookup )
            {
                currentEgoComponent = kvp.Key;
                var bundle = kvp.Value as EgoBundle< C1 >;
                yield return ( currentEgoComponent, bundle.component1, childConstraint as CS1 );
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return this;
        }
    }
}