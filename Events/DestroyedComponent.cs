using UnityEngine;

public class DestroyedComponent<C> : EgoEvent
    where C : Component
{
    public readonly C component;
    public readonly EgoComponent egoComponent;

    public DestroyedComponent( C component, EgoComponent egoComponent )
    {
        this.component = component;
        this.egoComponent = egoComponent;
    }
}
