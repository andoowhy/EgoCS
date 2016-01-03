using UnityEngine;

public class EgoBundle<C1> : EgoBundle
    where C1 : Component
{
    public readonly EgoComponent egoComponent;
    public readonly C1 component1;

    public EgoBundle( EgoComponent egoComponent, C1 component1 )
    {
        this.egoComponent = egoComponent;
        this.component1 = component1;
    }
}
