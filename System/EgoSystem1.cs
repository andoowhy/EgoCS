using System;
using UnityEngine;

public class EgoSystem<EC> : EgoSystem
    where EC : EgoConstraint, new()
{
    protected EC constraint;

    public EgoSystem()
    {
		constraint = new EC();
		constraint.SetSystem( this );
		EgoEvents<AddedGameObject>.AddHandler( Handle );
    }

    public override void CreateBundles( EgoComponent egoComponent )
    {
        constraint.CreateBundles( egoComponent );
    }

	protected void Handle( AddedGameObject e )
	{
		constraint.CreateBundles( e.egoComponent );
	}
}
