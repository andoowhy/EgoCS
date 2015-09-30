using UnityEngine;

public class AddedGameObject : EgoEvent
{
    public readonly GameObject gameObject;
    public readonly EgoComponent egoComponent;

    public AddedGameObject( GameObject gameObject, EgoComponent egoComponent )
    {
        this.gameObject = gameObject;
        this.egoComponent = egoComponent;
    }
}
