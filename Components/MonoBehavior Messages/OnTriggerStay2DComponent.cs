using UnityEngine;

[RequireComponent( typeof( EgoComponent ) ) ]
public class OnTriggerStay2DComponent : MonoBehaviour
{
    EgoComponent egoComponent;

    void Awake()
    {
        egoComponent = GetComponent<EgoComponent>();
    }

    void OnTriggerStay2D( Collider2D collider2d )
    {
        var e = new TriggerStay2DEvent(egoComponent, collider2d.gameObject.GetComponent<EgoComponent>(), collider2d);
        EgoEvents<TriggerStay2DEvent>.AddEvent( e );
    }
}
