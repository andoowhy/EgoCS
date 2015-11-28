using UnityEngine;

[RequireComponent(typeof(EgoComponent))]
public class OnTriggerStayComponent: MonoBehaviour
{
    void OnTriggerStay( Collider collider )
    {
        var thisEgoComponent = GetComponent<EgoComponent>();
        var otherEgoComponent = collider.gameObject.GetComponent<EgoComponent>();
        var e = new TriggerStay( thisEgoComponent, otherEgoComponent, collider );
        EgoEvents<TriggerStay>.AddEvent( e );
    }
}
