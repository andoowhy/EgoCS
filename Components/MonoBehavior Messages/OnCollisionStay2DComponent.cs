using UnityEngine;

[RequireComponent( typeof( EgoComponent ) )]
public class OnCollisionStay2DComponent : MonoBehaviour
{
    void OnCollisionStay2D( Collision2D collision )
    {
        var thisEgoComponent = GetComponent<EgoComponent>();
        var otherEgoComponent =  collision.gameObject.GetComponent<EgoComponent>();
        var e = new OnCollisionStay2D( thisEgoComponent, otherEgoComponent, collision );
        EgoEvents<OnCollisionStay2D>.AddEvent( e );
    }
}
