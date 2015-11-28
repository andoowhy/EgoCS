using UnityEngine;

[RequireComponent(typeof(EgoComponent))]
public class OnCollisionStayComponent : MonoBehaviour
{
    void OnCollisionStay( Collision collision )
    {
        var thisEgoComponent = GetComponent<EgoComponent>();
        var otherEgoComponent = collision.gameObject.GetComponent<EgoComponent>();
        var e = new CollisionStay( thisEgoComponent, otherEgoComponent, collision );
        EgoEvents<CollisionStay>.AddEvent( e );
    }
}
