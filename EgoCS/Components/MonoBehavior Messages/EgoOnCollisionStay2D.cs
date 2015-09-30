using UnityEngine;

[RequireComponent( typeof( EgoComponent ) )]
public class EgoOnCollisionStay2D : MonoBehaviour
{
    void OnCollisionStay2D( Collision2D collision )
    {
        var thisEgoComponent = GetComponent<EgoComponent>();
        var otherEgoComponent =  collision.gameObject.GetComponent<EgoComponent>();
        var e = new CollisionStay2D( thisEgoComponent, otherEgoComponent, collision );
        EgoEvents<CollisionStay2D>.Queue( e );
    }
}
