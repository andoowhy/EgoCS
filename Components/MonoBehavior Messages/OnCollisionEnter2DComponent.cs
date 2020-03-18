using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( EgoComponent ) )]
public class OnCollisionEnter2DComponent : MonoBehaviour
{
    public List< Collision2D > collisionEnter2Ds = new List< Collision2D >();

    void OnCollisionEnter2D( Collision2D collision )
    {
        collisionEnter2Ds.Add( collision );
    }
}