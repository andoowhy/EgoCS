using UnityEngine;

[RequireComponent( typeof( EgoComponent ) )]
public class OnTriggerExitComponent : MonoBehaviour
{
    void OnTriggerExit( Collider collider )
    {
        var thisEgoComponent = GetComponent<EgoComponent>();
        var otherEgoComponent =  collider.gameObject.GetComponent<EgoComponent>();
        var e = new TriggerExitEvent( thisEgoComponent, otherEgoComponent, collider );
        EgoEvents<TriggerExitEvent>.AddEvent( e );
    }
}
