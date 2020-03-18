using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( EgoComponent ) )]
public class OnCollisionStay2DComponent : MonoBehaviour
{
    public List< Collision2D > collisionStay2Ds = new List< Collision2D >();

    void OnCollisionStay2D( Collision2D collision )
    {
        collisionStay2Ds.Add( collision );
    }
}