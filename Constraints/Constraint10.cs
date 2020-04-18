namespace EgoCS
{
    using UnityEngine;
    using System.Collections.Generic;
    using System.Collections;

    public class Constraint< TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9, TComponent10 > : Constraint, IEnumerable< (EgoComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9, TComponent10 ) >
        where TComponent1 : Component
        where TComponent2 : Component
        where TComponent3 : Component
        where TComponent4 : Component
        where TComponent5 : Component
        where TComponent6 : Component
        where TComponent7 : Component
        where TComponent8 : Component
        where TComponent9 : Component
        where TComponent10 : Component
    {
        public Constraint()
        {
            _mask[ ComponentUtils.Get< TComponent1 >() ] = true;
            _mask[ ComponentUtils.Get< TComponent2 >() ] = true;
            _mask[ ComponentUtils.Get< TComponent3 >() ] = true;
            _mask[ ComponentUtils.Get< TComponent4 >() ] = true;
            _mask[ ComponentUtils.Get< TComponent5 >() ] = true;
            _mask[ ComponentUtils.Get< TComponent6 >() ] = true;
            _mask[ ComponentUtils.Get< TComponent7 >() ] = true;
            _mask[ ComponentUtils.Get< TComponent8 >() ] = true;
            _mask[ ComponentUtils.Get< TComponent9 >() ] = true;
            _mask[ ComponentUtils.Get< TComponent10 >() ] = true;
            _mask[ ComponentUtils.Get< EgoComponent >() ] = true;
        }

        protected override Bundle CreateBundle( EgoComponent egoComponent )
        {
            return new Bundle< TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9, TComponent10 >(
                egoComponent.GetComponent< TComponent1 >(),
                egoComponent.GetComponent< TComponent2 >(),
                egoComponent.GetComponent< TComponent3 >(),
                egoComponent.GetComponent< TComponent4 >(),
                egoComponent.GetComponent< TComponent5 >(),
                egoComponent.GetComponent< TComponent6 >(),
                egoComponent.GetComponent< TComponent7 >(),
                egoComponent.GetComponent< TComponent8 >(),
                egoComponent.GetComponent< TComponent9 >(),
                egoComponent.GetComponent< TComponent10 >()
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

            egoCS.AddAddedComponentCallback( typeof( TComponent5 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( TComponent5 ), CreateBundles );

            egoCS.AddAddedComponentCallback( typeof( TComponent6 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( TComponent6 ), CreateBundles );

            egoCS.AddAddedComponentCallback( typeof( TComponent7 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( TComponent7 ), CreateBundles );

            egoCS.AddAddedComponentCallback( typeof( TComponent8 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( TComponent8 ), CreateBundles );

            egoCS.AddAddedComponentCallback( typeof( TComponent9 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( TComponent9 ), CreateBundles );

            egoCS.AddAddedComponentCallback( typeof( TComponent10 ), CreateBundles );
            egoCS.AddDestroyedComponentCallback( typeof( TComponent10 ), CreateBundles );
        }

        IEnumerator< (EgoComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9, TComponent10 ) > IEnumerable< (EgoComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9, TComponent10) >.GetEnumerator()
        {
            var lookup = GetLookup( rootBundles );
            foreach( var kvp in lookup )
            {
                currentEgoComponent = kvp.Key;
                var bundle = kvp.Value as Bundle< TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9, TComponent10 >;
                yield return ( currentEgoComponent, bundle.component1, bundle.component2, bundle.component3, bundle.component4, bundle.component5, bundle.component6, bundle.component7, bundle.component8, bundle.component9, bundle.component10 );
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return this;
        }
    }
}