using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( EgoComponent ) )]
public class OnCollisionExit2DComponent : MonoBehaviour
{
    public List< Collision2D > collisionExit2Ds = new List< Collision2D >();

    void OnCollisionExit2D( Collision2D collision )
    {
        collisionExit2Ds.Add( collision );
    }
}