using UnityEngine;

[RequireComponent( typeof( EgoComponent ) )]
public class EgoOnCollisionExit2D : MonoBehaviour
{
    void OnCollisionExit2D( Collision2D collision )
    {
        var thisEgoComponent = GetComponent<EgoComponent>();
        var otherEgoComponent =  collision.gameObject.GetComponent<EgoComponent>();
        var e = new CollisionExit2D( thisEgoComponent, otherEgoComponent, collision );
        EgoEvents<CollisionExit2D>.Queue( e );
    }
}
