namespace EgoCS
{
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent( typeof( EgoComponent ) )]
    public class OnTriggerExit2DComponent : MonoBehaviour
    {
        public List< Collider2D > triggerExit2Ds = new List< Collider2D >();

        void OnTriggerExit2D( Collider2D collider2d )
        {
            triggerExit2Ds.Add( collider2d );
        }
    }
}