using UnityEngine;

public class TriggerExit2DEvent : EgoEvent
{
    public readonly EgoComponent egoComponent1;
    public readonly EgoComponent egoComponent2;
    public readonly Collider2D collider;

    public TriggerExit2DEvent( EgoComponent egoComponent1, EgoComponent egoComponent2, Collider2D collider )
    {
        this.egoComponent1 = egoComponent1;
        this.egoComponent2 = egoComponent2;
        this.collider = collider;
    }
}

