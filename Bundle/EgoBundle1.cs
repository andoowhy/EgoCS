using UnityEngine;

public class EgoBundle<C1> : EgoBundle
    where C1 : Component
{
    public readonly Transform transform;
    public readonly C1 component1;

    public EgoBundle( Transform transform, C1 component1 )
    {
        this.transform = transform;
        this.component1 = component1;
    }
}
