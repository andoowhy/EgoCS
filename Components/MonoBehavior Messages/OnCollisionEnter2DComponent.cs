using UnityEngine;

[RequireComponent(typeof(EgoComponent))]
public class OnCollisionEnter2DComponent : MonoBehaviour
{
    void OnCollisionEnter2D( Collision2D collision )
    {
        var thisEgoComponent = GetComponent<EgoComponent>();
        var otherEgoComponent =  collision.gameObject.GetComponent<EgoComponent>();
        var e = new CollisionEnter2DEvent( thisEgoComponent, otherEgoComponent, collision );
        EgoEvents<CollisionEnter2DEvent>.AddEvent( e );
    }
}
