using UnityEngine;

[RequireComponent( typeof( EgoComponent ) ) ]
public class OnCollisionStay2DComponent : MonoBehaviour
{
    EgoComponent egoComponent;

    void Awake()
    {
        egoComponent = GetComponent<EgoComponent>();
    }

    void OnCollisionStay2D( Collision2D collision )
    {
        var e = new CollisionStay2DEvent( egoComponent, collision.gameObject.GetComponent<EgoComponent>(), collision );
        EgoEvents<CollisionStay2DEvent>.AddEvent( e );
    }
}
