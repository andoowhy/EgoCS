using UnityEngine;

[RequireComponent( typeof( EgoComponent ) ) ]
public class OnTriggerEnterComponent : MonoBehaviour
{
    EgoComponent egoComponent;

    void Awake()
    {
        egoComponent = GetComponent<EgoComponent>();
    }

    void OnTriggerEnter( Collider collider )
    {
        var e = new TriggerEnterEvent( egoComponent, collider.gameObject.GetComponent<EgoComponent>(), collider );
        EgoEvents<TriggerEnterEvent>.AddEvent( e );
    }
}
