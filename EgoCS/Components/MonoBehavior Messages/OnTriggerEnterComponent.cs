using UnityEngine;

[RequireComponent(typeof(EgoComponent))]
public class OnTriggerEnterComponent : MonoBehaviour
{
    void OnTriggerEnter( Collider collider )
    {
        var egoComponent1 = GetComponent<EgoComponent>();
        var egoComponent2 = collider.gameObject.GetComponent<EgoComponent>();
        var e = new TriggerEnter( egoComponent1, egoComponent2, collider );
        EgoEvents<TriggerEnter>.AddEvent( e );
    }
}
