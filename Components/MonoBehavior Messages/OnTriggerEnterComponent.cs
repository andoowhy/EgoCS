namespace EgoCS
{
    using System.Collections.Generic;
    using UnityEngine;

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