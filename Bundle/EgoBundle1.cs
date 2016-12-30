using UnityEngine;

public class EgoBundle<C1> : EgoBundle
    where C1 : Component
{ 
    public readonly C1 component1;

    public EgoBundle( C1 component1 )
    {
        this.component1 = component1;
    }
}
