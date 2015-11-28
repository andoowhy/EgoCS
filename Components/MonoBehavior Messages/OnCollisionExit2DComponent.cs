using UnityEngine;

[RequireComponent( typeof( EgoComponent ) )]
public class OnCollisionExit2DComponent : MonoBehaviour
{
    void OnCollisionExit2D( Collision2D collision )
    {
        var thisEgoComponent = GetComponent<EgoComponent>();
        var otherEgoComponent =  collision.gameObject.GetComponent<EgoComponent>();
        var e = new OnCollisionExit2D( thisEgoComponent, otherEgoComponent, collision );
        EgoEvents<OnCollisionExit2D>.AddEvent( e );
    }
}
