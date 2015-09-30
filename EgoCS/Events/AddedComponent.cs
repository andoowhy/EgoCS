using UnityEngine;

public class AddedComponent<C> : EgoEvent
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
