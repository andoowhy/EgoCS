using UnityEngine;

public class TriggerExit : EgoEvent
{
    public readonly EgoComponent egoComponent1;
    public readonly EgoComponent egoComponent2;
    public readonly Collider collider;

    public TriggerExit( EgoComponent egoComponent1, EgoComponent egoComponent2, Collider collider )
    {
        this.egoComponent1 = egoComponent1;
        this.egoComponent2 = egoComponent2;
        this.collider = collider;
    }
}

