﻿using UnityEngine;

public class CollisionStayEvent : IEgoEvent
{
    public readonly EgoComponent egoComponent1;
    public readonly EgoComponent egoComponent2;
    public readonly Collision collision;

    public CollisionStayEvent( EgoComponent egoComponent1, EgoComponent egoComponent2, Collision collision )
    {
        this.egoComponent1 = egoComponent1;
        this.egoComponent2 = egoComponent2;
        this.collision = collision;
    }
}
