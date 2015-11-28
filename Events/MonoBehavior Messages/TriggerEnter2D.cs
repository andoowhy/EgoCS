using UnityEngine;

public class TriggerEnter2D : EgoEvent
{
    public readonly EgoComponent egoComponent1;
    public readonly EgoComponent egoComponent2;
    public readonly Collider2D collider;

    public TriggerEnter2D( EgoComponent egoComponent1, EgoComponent egoComponent2, Collider2D collider )
    {
        this.egoComponent1 = egoComponent1;
        this.egoComponent2 = egoComponent2;
        this.collider = collider;
    }
}

