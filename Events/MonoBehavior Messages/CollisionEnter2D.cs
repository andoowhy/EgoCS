using UnityEngine;

public class CollisionEnter2DEvent : EgoEvent
{
    public readonly EgoComponent egoComponent1;
    public readonly EgoComponent egoComponent2;
    public readonly Collision2D collision;

    public CollisionEnter2DEvent( EgoComponent egoComponent1, EgoComponent egoComponent2, Collision2D collision )
    {
        this.egoComponent1 = egoComponent1;
        this.egoComponent2 = egoComponent2;
        this.collision = collision;
    }
}