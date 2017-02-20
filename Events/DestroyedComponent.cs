using UnityEngine;

public abstract class DestroyedComponent : EgoEvent { }

public class DestroyedComponent<C> : DestroyedComponent
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
