using UnityEngine;

public class DestroyedGameObject : EgoEvent
{
    public readonly GameObject gameObject;
    public readonly EgoComponent egoComponent;

    public DestroyedGameObject( GameObject gameObject, EgoComponent egoComponent )
    {
        this.gameObject = gameObject;
        this.egoComponent = egoComponent;
    }
}
