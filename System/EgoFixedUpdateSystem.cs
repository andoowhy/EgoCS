using System;

public abstract class EgoFixedUpdateSystem : EgoSystem { }

public abstract class EgoFixedUpdateSystem< EI > : EgoFixedUpdateSystem
    where EI : EgoInterface
{
    public abstract void FixedUpdate( EI egoInterface );
}

public abstract class EgoFixedUpdateSystem< EI, EC > : EgoFixedUpdateSystem< EI >
    where EI : EgoInterface, new()
    where EC : EgoConstraint, new()
{
    protected EC constraint;

    public EgoFixedUpdateSystem()
    {
        constraint = new EC();
        constraint.SetSystem( this );
        EgoEvents< AddedGameObject >.AddHandler( e => constraint.CreateBundles( e.egoComponent ) );
        EgoEvents< DestroyedGameObject >.AddHandler( e => constraint.RemoveBundles( e.egoComponent ) );
    }

    public override void CreateBundles( EgoComponent egoComponent )
    {
        constraint.CreateBundles( egoComponent );
    }
}