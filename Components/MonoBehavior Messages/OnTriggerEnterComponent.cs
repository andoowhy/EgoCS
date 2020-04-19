using System.Collections.Generic;
using UnityEngine;

namespace EgoCS
{
    [RequireComponent( typeof( EgoComponent ) )]
    public class OnTriggerEnterComponent : MonoBehaviour
    {
        public List< Collider > triggerEnters = new List< Collider >();

        void OnTriggerEnter( Collider collider )
        {
            triggerEnters.Add( collider );
        }
    }
}