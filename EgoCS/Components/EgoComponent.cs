using UnityEngine;

[DisallowMultipleComponent]
public class EgoComponent : MonoBehaviour
{
    public BitMask mask = new BitMask( ComponentIDs.size );

    public void CreateMask()
    {
        mask.SetAll( false );

        // Initialize the ECSInterface's mask from each attached Component
        var components = gameObject.GetComponents<Component>();
        foreach( var component in components )
        {
            var componentID = ComponentIDs.types[ component.GetType() ];
            mask[componentID] = true;
        }
    }    

    void OnCollisionExit2D( Collision2D collision )
    {
        var e = new CollisionExit2D( this, collision.gameObject.GetComponent<EgoComponent>(), collision );
        EgoEvents<CollisionExit2D>.Queue( e );
    }

    void OnCollisionStay2D( Collision2D collision )
    {
        var e = new CollisionStay2D( this, collision.gameObject.GetComponent<EgoComponent>(), collision );
        EgoEvents<CollisionStay2D>.Queue( e );
    }

    public bool HasComponent<C1>()
        where C1 : Component
    {
        return mask[ ComponentIDs<C1>.ID ];
    }

    public bool HasComponents<C1, C2>()
        where C1 : Component
        where C2 : Component
    {
        return mask[ComponentIDs<C1>.ID]
            && mask[ComponentIDs<C2>.ID];
    }

    public bool HasComponents<C1, C2, C3>()
       where C1 : Component
       where C2 : Component
       where C3 : Component
    {
        return mask[ComponentIDs<C1>.ID]
            && mask[ComponentIDs<C2>.ID]
            && mask[ComponentIDs<C3>.ID];
    }

    public bool HasComponents<C1, C2, C3, C4>()
       where C1 : Component
       where C2 : Component
       where C3 : Component
       where C4 : Component
    {
        return mask[ComponentIDs<C1>.ID]
            && mask[ComponentIDs<C2>.ID]
            && mask[ComponentIDs<C3>.ID]
            && mask[ComponentIDs<C4>.ID];
    }

    public bool HasComponents<C1, C2, C3, C4, C5>()
       where C1 : Component
       where C2 : Component
       where C3 : Component
       where C4 : Component
       where C5 : Component
    {
        return mask[ComponentIDs<C1>.ID]
            && mask[ComponentIDs<C2>.ID]
            && mask[ComponentIDs<C3>.ID]
            && mask[ComponentIDs<C4>.ID]
            && mask[ComponentIDs<C5>.ID];
    }
}