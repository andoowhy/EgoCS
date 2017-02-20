using UnityEngine;

public abstract class AddedComponent : EgoEvent{}

public class AddedComponent<C> : AddedComponent
    where C : Component
{
    public readonly C component;
    public readonly EgoComponent egoComponent;

    public AddedComponent( C component, EgoComponent egoComponent )
    {
        this.component = component;
        this.egoComponent = egoComponent;
    }
}
