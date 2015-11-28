using UnityEngine;

[RequireComponent( typeof( EgoComponent ) )]
public class OnTriggerExit2DComponent : MonoBehaviour
{
    void OnTriggerExit2D( Collider2D collider2d )
    {
        var thisEgoComponent = GetComponent<EgoComponent>();
        var otherEgoComponent =  collider2d.gameObject.GetComponent<EgoComponent>();
        var e = new TriggerExit2D( thisEgoComponent, otherEgoComponent, collider2d );
        EgoEvents<TriggerExit2D>.AddEvent( e );
    }
}
