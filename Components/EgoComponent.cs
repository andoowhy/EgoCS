using UnityEngine;
using System;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class EgoComponent : MonoBehaviour
{
	BitMask _mask = new BitMask( ComponentIDs.GetCount() );
	public BitMask mask
	{
		get { return _mask; }
	}

    public EgoComponent parent
    {
        get
        {
            var parentTransform = transform.parent;
            return ( parentTransform != null ) ? parentTransform.GetComponent<EgoComponent>() : null;
        }
    }

    public EgoComponent[] children
    {
        get
        {
            var childCount = transform.childCount;
            var children = new EgoComponent[ childCount ];
            for( int i = 0; i < childCount; i++ )
            {
                children[ i ] = transform.GetChild( i ).GetComponent<EgoComponent>();
            }
            return children;
        }
    }

    public void CreateMask()
    {
        mask.SetAll( false );

        // Initialize the ECSInterface's mask from each attached Component
        var components = gameObject.GetComponents<Component>();
        foreach( var component in components )
        {
            mask[ ComponentIDs.Get( component.GetType() ) ] = true;
        }
    }

	#region HasComponents
	public bool HasComponents<C1>()
        where C1 : Component
    {
        return mask[ ComponentIDs.Get( typeof(C1) ) ];
    }

    public bool HasComponents<C1, C2>()
        where C1 : Component
        where C2 : Component
    {
        return mask[ ComponentIDs.Get( typeof( C1 ) ) ]
            && mask[ ComponentIDs.Get( typeof( C2 ) ) ];
    }

    public bool HasComponents<C1, C2, C3>()
       where C1 : Component
       where C2 : Component
       where C3 : Component
    {
        return mask[ComponentIDs.Get( typeof( C1 ) ) ]
            && mask[ComponentIDs.Get( typeof( C2 ) ) ]
            && mask[ComponentIDs.Get( typeof( C3 ) ) ];
    }

    public bool HasComponents<C1, C2, C3, C4>()
       where C1 : Component
       where C2 : Component
       where C3 : Component
       where C4 : Component
    {
        return mask[ComponentIDs.Get( typeof( C1 ) ) ]
            && mask[ComponentIDs.Get( typeof( C2 ) ) ]
            && mask[ComponentIDs.Get( typeof( C3 ) ) ]
            && mask[ComponentIDs.Get( typeof( C4 ) ) ];
    }

    public bool HasComponents<C1, C2, C3, C4, C5>()
       where C1 : Component
       where C2 : Component
       where C3 : Component
       where C4 : Component
       where C5 : Component
    {
        return mask[ComponentIDs.Get( typeof( C1 ) ) ]
            && mask[ComponentIDs.Get( typeof( C2 ) ) ]
            && mask[ComponentIDs.Get( typeof( C3 ) ) ]
            && mask[ComponentIDs.Get( typeof( C4 ) ) ]
            && mask[ComponentIDs.Get( typeof( C5 ) ) ];
    }
	#endregion

	#region TryGetComponents
	public bool TryGetComponents<C1>( out C1 component1 )
        where C1 : Component
    {
        if( HasComponents<C1>() )
        {
            component1 = GetComponent<C1>();
            return true;
        }
        else
        {
            component1 = null;
            return false;
        }
    }

    public bool TryGetComponents<C1, C2>( out C1 component1, out C2 component2 )
        where C1 : Component
        where C2 : Component
    {
        if( HasComponents<C1, C2>() )
        {
            component1 = GetComponent<C1>();
            component2 = GetComponent<C2>();
            return true;
        }
        else
        {
            component1 = null;
            component2 = null;
            return false;
        }
    }

    public bool TryGetComponents<C1, C2, C3>( out C1 component1, out C2 component2, out C3 component3 )
        where C1 : Component
        where C2 : Component
        where C3 : Component
    {
        if( HasComponents<C1, C2, C3>() )
        {
            component1 = GetComponent<C1>();
            component2 = GetComponent<C2>();
            component3 = GetComponent<C3>();
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

    public bool TryGetComponents<C1, C2, C3, C4>( out C1 component1, out C2 component2, out C3 component3, out C4 component4 )
        where C1 : Component
        where C2 : Component
        where C3 : Component
        where C4 : Component
    {
        if( HasComponents<C1, C2, C3, C4>() )
        {
            component1 = GetComponent<C1>();
            component2 = GetComponent<C2>();
            component3 = GetComponent<C3>();
            component4 = GetComponent<C4>();
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

    public bool TryGetComponents<C1, C2, C3, C4, C5>( out C1 component1, out C2 component2, out C3 component3, out C4 component4, out C5 component5 )
        where C1 : Component
        where C2 : Component
        where C3 : Component
        where C4 : Component
        where C5 : Component
    {
        if( HasComponents<C1, C2, C3, C4, C5>() )
        {
            component1 = GetComponent<C1>();
            component2 = GetComponent<C2>();
            component3 = GetComponent<C3>();
            component4 = GetComponent<C4>();
            component5 = GetComponent<C5>();
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