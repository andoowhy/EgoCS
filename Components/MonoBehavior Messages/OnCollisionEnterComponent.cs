using UnityEngine;

[RequireComponent( typeof( EgoComponent ) ) ]
public class OnCollisionEnterComponent : MonoBehaviour
{
    EgoComponent egoComponent;

    void Awake()
    {
        egoComponent = GetComponent<EgoComponent>();
    }

    void OnCollisionEnter( Collision collision )
    {
        var e = new CollisionEnterEvent( egoComponent, collision.gameObject.GetComponent<EgoComponent>(), collision );
        EgoEvents<CollisionEnterEvent>.AddEvent( e );
    }
}
