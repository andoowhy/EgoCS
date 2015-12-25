using UnityEngine;

[RequireComponent(typeof(EgoComponent))]
public class OnCollisionEnterComponent : MonoBehaviour
{
    void OnCollisionEnter( Collision collision )
    {
        var egoComponent1 = GetComponent<EgoComponent>();
        var egoComponent2 = collision.gameObject.GetComponent<EgoComponent>();
        var e = new CollisionEnterEvent( egoComponent1, egoComponent2, collision );
        EgoEvents<CollisionEnterEvent>.AddEvent( e );
    }
}
