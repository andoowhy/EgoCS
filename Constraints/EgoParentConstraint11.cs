namespace EgoCS
{
    using System.Collections;
    using UnityEngine;
    using System.Collections.Generic;

    public class EgoParentConstraint< C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, CS1 > : EgoParentConstraint, IEnumerable< (EgoComponent, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, CS1) >
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
            _mask[ ComponentUtils.Get< C5 >() ] = true;
            _mask[ ComponentUtils.Get< C6 >() ] = true;
            _mask[ ComponentUtils.Get< C7 >() ] = true;
            _mask[ ComponentUtils.Get< C8 >() ] = true;
            _mask[ ComponentUtils.Get< C9 >() ] = true;
            _mask[ ComponentUtils.Get< C10 >() ] = true;
            _mask[ ComponentUtils.Get< C11 >() ] = true;
            _mask[ ComponentUtils.Get< EgoComponent >() ] = true;
        }

        protected override EgoBundle CreateBundle( EgoComponent egoComponent )
        {
            return new EgoBundle< C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11 >(
                egoComponent.GetComponent< C1 >(),
                egoComponent.GetComponent< C2 >(),
                egoComponent.GetComponent< C3 >(),
                egoComponent.GetComponent< C4 >(),
                egoComponent.GetComponent< C5 >(),
                egoComponent.GetComponent< C6 >(),
                egoComponent.GetComponent< C7 >(),
                egoComponent.GetComponent< C8 >(),
                egoComponent.GetComponent< C9 >(),
                egoComponent.GetComponent< C10 >(),
                egoComponent.GetComponent< C11 >()
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

            egoCS.AddAddedComponentCallback( typeof( C5 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( C5 ), CreateBundles );

            egoCS.AddAddedComponentCallback( typeof( C6 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( C6 ), CreateBundles );

            egoCS.AddAddedComponentCallback( typeof( C7 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( C7 ), CreateBundles );

            egoCS.AddAddedComponentCallback( typeof( C8 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( C8 ), CreateBundles );

            egoCS.AddAddedComponentCallback( typeof( C9 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( C9 ), CreateBundles );

            egoCS.AddAddedComponentCallback( typeof( C10 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( C10 ), CreateBundles );

            egoCS.AddAddedComponentCallback( typeof( C11 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( C11 ), CreateBundles );

            egoCS.AddSetParentCallback( SetParent );
        }

        IEnumerator< (EgoComponent, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, CS1) > IEnumerable< (EgoComponent, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, CS1) >.GetEnumerator()
        {
            var lookup = GetLookup( rootBundles );
            foreach( var kvp in lookup )
            {
                currentEgoComponent = kvp.Key;
                var bundle = kvp.Value as EgoBundle< C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11 >;
                yield return ( currentEgoComponent, bundle.component1, bundle.component2, bundle.component3, bundle.component4, bundle.component5, bundle.component6, bundle.component7, bundle.component8, bundle.component9, bundle.component10, bundle.component11, childConstraint as CS1 );
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return this;
        }
    }
}