using UnityEngine;

[RequireComponent( typeof( EgoComponent ) )]
public class OnCollisionExit2DComponent : MonoBehaviour
{
    void OnCollisionExit2D( Collision2D collision )
    {
        var thisEgoComponent = GetComponent<EgoComponent>();
        var otherEgoComponent =  collision.gameObject.GetComponent<EgoComponent>();
        var e = new CollisionExit2DEvent( thisEgoComponent, otherEgoComponent, collision );
        EgoEvents<CollisionExit2DEvent>.AddEvent( e );
    }
}
