using UnityEngine;

[RequireComponent(typeof(EgoComponent))]
public class OnCollisionEnter2DComponent : MonoBehaviour
{
    void OnCollisionEnter2D( Collision2D collision )
    {
        var thisEgoComponent = GetComponent<EgoComponent>();
        var otherEgoComponent =  collision.gameObject.GetComponent<EgoComponent>();
        var e = new OnCollisionEnter2D( thisEgoComponent, otherEgoComponent, collision );
        EgoEvents<OnCollisionEnter2D>.AddEvent( e );
    }
}
