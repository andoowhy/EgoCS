using UnityEngine;

public class TriggerStay : EgoEvent
{
    public readonly EgoComponent egoComponent1;
    public readonly EgoComponent egoComponent2;
    public readonly Collider collider;

    public TriggerStay( EgoComponent egoComponent1, EgoComponent egoComponent2, Collider collider )
    {
        this.egoComponent1 = egoComponent1;
        this.egoComponent2 = egoComponent2;
        this.collider = collider;
    }
}

