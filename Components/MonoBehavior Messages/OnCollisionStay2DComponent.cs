using UnityEngine;

[RequireComponent( typeof( EgoComponent ) )]
public class OnCollisionStay2DComponent : MonoBehaviour
{
    void OnCollisionStay2D( Collision2D collision )
    {
        var thisEgoComponent = GetComponent<EgoComponent>();
        var otherEgoComponent =  collision.gameObject.GetComponent<EgoComponent>();
        var e = new CollisionStay2DEvent( thisEgoComponent, otherEgoComponent, collision );
        EgoEvents<CollisionStay2DEvent>.AddEvent( e );
    }
}
