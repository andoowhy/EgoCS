using System;
using UnityEngine;

public class EgoSystem<EC> : EgoSystem
    where EC : EgoConstraint, new()
{
    protected EC constraint;

    public EgoSystem()
    {
        constraint = new EC();
    }

    public override void CreateBundles( EgoComponent egoComponent )
    {
        constraint.CreateBundles( egoComponent );
    }
}
