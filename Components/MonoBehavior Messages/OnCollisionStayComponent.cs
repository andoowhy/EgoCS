using System.Collections.Generic;
using UnityEngine;

namespace EgoCS
{
    [RequireComponent( typeof( EgoComponent ) )]
    public class OnCollisionStayComponent : MonoBehaviour
    {
        public List< Collision > collisionStays = new List< Collision >();

        void OnCollisionStay( Collision collision )
        {
            collisionStays.Add( collision );
        }
    }
}