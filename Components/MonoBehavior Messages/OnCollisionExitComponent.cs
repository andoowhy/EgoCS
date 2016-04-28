using UnityEngine;

[RequireComponent( typeof( EgoComponent ) ) ]
public class OnCollisionExitComponent : MonoBehaviour
{
    EgoComponent egoComponent;

    void Awake()
    {
        egoComponent = GetComponent<EgoComponent>();
    }

    void OnCollisionExit( Collision collision )
    {
        var e = new CollisionExitEvent( egoComponent, collision.gameObject.GetComponent<EgoComponent>(), collision );
        EgoEvents<CollisionExitEvent>.AddEvent( e );
    }
}
