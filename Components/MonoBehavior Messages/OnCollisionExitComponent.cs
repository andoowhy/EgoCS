using System.Collections.Generic;
using UnityEngine;

namespace EgoCS
{
    [RequireComponent( typeof( EgoComponent ) )]
    public class OnCollisionExitComponent : MonoBehaviour
    {
        public List< Collision > collisionExits = new List< Collision >();

        void OnCollisionExit( Collision collision )
        {
            collisionExits.Add( collision );
        }
    }
}