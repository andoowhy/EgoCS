using UnityEngine;

[RequireComponent(typeof(EgoComponent))]
public class OnTriggerStayComponent: MonoBehaviour
{
    void OnTriggerStay( Collider collider )
    {
        var thisEgoComponent = GetComponent<EgoComponent>();
        var otherEgoComponent = collider.gameObject.GetComponent<EgoComponent>();
        var e = new TriggerStayEvent( thisEgoComponent, otherEgoComponent, collider );
        EgoEvents<TriggerStayEvent>.AddEvent( e );
    }
}
