namespace EgoCS
{
    using System.Collections.Generic;
    using UnityEngine;

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