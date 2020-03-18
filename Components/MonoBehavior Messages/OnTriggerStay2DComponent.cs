using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( EgoComponent ) )]
public class OnTriggerStay2DComponent : MonoBehaviour
{
    public List< Collider2D > triggerStay2Ds = new List< Collider2D >();

    void OnTriggerStay2D( Collider2D collider2d )
    {
        triggerStay2Ds.Add( collider2d );
    }
}