using UnityEngine;

[RequireComponent(typeof(EgoComponent))]
public class OnTriggerEnter2DComponent : MonoBehaviour
{
    void OnTriggerEnter2D( Collider2D collider2d )
    {
        var thisEgoComponent = GetComponent<EgoComponent>();
        var otherEgoComponent =  collider2d.gameObject.GetComponent<EgoComponent>();
        var e = new TriggerEnter2D( thisEgoComponent, otherEgoComponent, collider2d );
        EgoEvents<TriggerEnter2D>.AddEvent( e );
    }
}
