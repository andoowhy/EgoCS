using UnityEngine;

[RequireComponent( typeof( EgoComponent ) ) ]
public class OnCollisionEnter2DComponent : MonoBehaviour
{
    EgoComponent egoComponent;

    void Awake()
    {
        egoComponent = GetComponent<EgoComponent>();
    }

    void OnCollisionEnter2D( Collision2D collision )
    {
        var e = new CollisionEnter2DEvent( egoComponent, collision.gameObject.GetComponent<EgoComponent>(), collision );
        EgoEvents<CollisionEnter2DEvent>.AddEvent( e );
    }
}
