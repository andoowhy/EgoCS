using UnityEngine;

[RequireComponent( typeof( EgoComponent ) )]
public class OnTriggerStay2DComponent : MonoBehaviour
{
    void OnTriggerStay2D( Collider2D collider2d )
    {
        var thisEgoComponent = GetComponent<EgoComponent>();
        var otherEgoComponent =  collider2d.gameObject.GetComponent<EgoComponent>();
        var e = new TriggerStay2D( thisEgoComponent, otherEgoComponent, collider2d );
        EgoEvents<TriggerStay2D>.AddEvent( e );
    }
}
