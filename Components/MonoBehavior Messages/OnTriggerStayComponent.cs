namespace EgoCS
{
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent( typeof( EgoComponent ) )]
    public class OnTriggerStayComponent : MonoBehaviour
    {
        public List< Collider > triggerStays = new List< Collider >();

        void OnTriggerStay( Collider collider )
        {
            triggerStays.Add( collider );
        }
    }
}