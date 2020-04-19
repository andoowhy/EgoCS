using System.Collections.Generic;
using UnityEngine;

namespace EgoCS
{
    [RequireComponent( typeof( EgoComponent ) )]
    public class OnTriggerEnter2DComponent : MonoBehaviour
    {
        public List< Collider2D > triggerEnter2Ds = new List< Collider2D >();

        void OnTriggerEnter2D( Collider2D collider2d )
        {
            triggerEnter2Ds.Add( collider2d );
        }
    }
}