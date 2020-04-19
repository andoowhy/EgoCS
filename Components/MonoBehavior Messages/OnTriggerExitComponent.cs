using System.Collections.Generic;
using UnityEngine;

namespace EgoCS
{
    [RequireComponent( typeof( EgoComponent ) )]
    public class OnTriggerExitComponent : MonoBehaviour
    {
        public List< Collider > triggerExits = new List< Collider >();

        void OnTriggerExit( Collider collider )
        {
            triggerExits.Add( collider );
        }
    }
}