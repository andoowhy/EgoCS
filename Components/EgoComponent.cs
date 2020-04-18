namespace EgoCS
{
    using UnityEngine;

    [DisallowMultipleComponent]
    public class EgoComponent : MonoBehaviour
    {
        BitMask _mask = new BitMask( ComponentUtils.GetCount() );

        public BitMask mask
        {
            get { return _mask; }
        }

        public EgoComponent parent
        {
            get
            {
                var parentTransform = transform.parent;
                return ( parentTransform != null ) ? parentTransform.GetComponent< EgoComponent >() : null;
            }
        }

        public EgoComponent[] children
        {
            get
            {
                var childCount = transform.childCount;
                var children = new EgoComponent[childCount];
                for( int i = 0; i < childCount; i++ )
                {
                    children[ i ] = transform.GetChild( i ).GetComponent< EgoComponent >();
                }

                return children;
            }
        }

        public void CreateMask()
        {
            mask.SetAll( false );

            // Initialize the ECSInterface's mask from each attached Component
            var components = gameObject.GetComponents< Component >();
            foreach( var component in components )
            {
                mask[ ComponentUtils.Get( component.GetType() ) ] = true;
            }
        }

        #region HasComponents

        public bool HasComponents< TComponent1 >()
            where TComponent1 : Component
        {
            return mask[ ComponentUtils.Get( typeof( TComponent1 ) ) ];
        }

        public bool HasComponents< TComponent1, TComponent2 >()
            where TComponent1 : Component
            where TComponent2 : Component
        {
            return mask[ ComponentUtils.Get( typeof( TComponent1 ) ) ]
                   && mask[ ComponentUtils.Get( typeof( TComponent2 ) ) ];
        }

        public bool HasComponents< TComponent1, TComponent2, TComponent3 >()
            where TComponent1 : Component
            where TComponent2 : Component
            where TComponent3 : Component
        {
            return mask[ ComponentUtils.Get( typeof( TComponent1 ) ) ]
                   && mask[ ComponentUtils.Get( typeof( TComponent2 ) ) ]
                   && mask[ ComponentUtils.Get( typeof( TComponent3 ) ) ];
        }

        public bool HasComponents< TComponent1, TComponent2, TComponent3, TComponent4 >()
            where TComponent1 : Component
            where TComponent2 : Component
            where TComponent3 : Component
            where TComponent4 : Component
        {
            return mask[ ComponentUtils.Get( typeof( TComponent1 ) ) ]
                   && mask[ ComponentUtils.Get( typeof( TComponent2 ) ) ]
                   && mask[ ComponentUtils.Get( typeof( TComponent3 ) ) ]
                   && mask[ ComponentUtils.Get( typeof( TComponent4 ) ) ];
        }

        public bool HasComponents< TComponent1, TComponent2, TComponent3, TComponent4, TComponent5 >()
            where TComponent1 : Component
            where TComponent2 : Component
            where TComponent3 : Component
            where TComponent4 : Component
            where TComponent5 : Component
        {
            return mask[ ComponentUtils.Get( typeof( TComponent1 ) ) ]
                   && mask[ ComponentUtils.Get( typeof( TComponent2 ) ) ]
                   && mask[ ComponentUtils.Get( typeof( TComponent3 ) ) ]
                   && mask[ ComponentUtils.Get( typeof( TComponent4 ) ) ]
                   && mask[ ComponentUtils.Get( typeof( TComponent5 ) ) ];
        }

        #endregion

        #region TryGetComponents

        public bool TryGetComponents< TComponent1 >( out TComponent1 component1 )
            where TComponent1 : Component
        {
            if( HasComponents< TComponent1 >() )
            {
                component1 = GetComponent< TComponent1 >();
                return true;
            }
            else
            {
                component1 = null;
                return false;
            }
        }

        public bool TryGetComponents< TComponent1, TComponent2 >( out TComponent1 component1, out TComponent2 component2 )
            where TComponent1 : Component
            where TComponent2 : Component
        {
            if( HasComponents< TComponent1, TComponent2 >() )
            {
                component1 = GetComponent< TComponent1 >();
                component2 = GetComponent< TComponent2 >();
                return true;
            }
            else
            {
                component1 = null;
                component2 = null;
                return false;
            }
        }

        public bool TryGetComponents< TComponent1, TComponent2, TComponent3 >( out TComponent1 component1, out TComponent2 component2, out TComponent3 component3 )
            where TComponent1 : Component
            where TComponent2 : Component
            where TComponent3 : Component
        {
            if( HasComponents< TComponent1, TComponent2, TComponent3 >() )
            {
                component1 = GetComponent< TComponent1 >();
                component2 = GetComponent< TComponent2 >();
                component3 = GetComponent< TComponent3 >();
                return true;
            }
            else
            {
                component1 = null;
                component2 = null;
                component3 = null;
                return false;
            }
        }

        public bool TryGetComponents< TComponent1, TComponent2, TComponent3, TComponent4 >( out TComponent1 component1, out TComponent2 component2, out TComponent3 component3, out TComponent4 component4 )
            where TComponent1 : Component
            where TComponent2 : Component
            where TComponent3 : Component
            where TComponent4 : Component
        {
            if( HasComponents< TComponent1, TComponent2, TComponent3, TComponent4 >() )
            {
                component1 = GetComponent< TComponent1 >();
                component2 = GetComponent< TComponent2 >();
                component3 = GetComponent< TComponent3 >();
                component4 = GetComponent< TComponent4 >();
                return true;
            }
            else
            {
                component1 = null;
                component2 = null;
                component3 = null;
                component4 = null;
                return false;
            }
        }

        public bool TryGetComponents< TComponent1, TComponent2, TComponent3, TComponent4, TComponent5 >( out TComponent1 component1, out TComponent2 component2, out TComponent3 component3, out TComponent4 component4, out TComponent5 component5 )
            where TComponent1 : Component
            where TComponent2 : Component
            where TComponent3 : Component
            where TComponent4 : Component
            where TComponent5 : Component
        {
            if( HasComponents< TComponent1, TComponent2, TComponent3, TComponent4, TComponent5 >() )
            {
                component1 = GetComponent< TComponent1 >();
                component2 = GetComponent< TComponent2 >();
                component3 = GetComponent< TComponent3 >();
                component4 = GetComponent< TComponent4 >();
                component5 = GetComponent< TComponent5 >();
                return true;
            }
            else
            {
                component1 = null;
                component2 = null;
                component3 = null;
                component4 = null;
                component5 = null;
                return false;
            }
        }

        #endregion
    }
}