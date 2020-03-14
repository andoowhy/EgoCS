using System;

public abstract class EgoUpdateSystem : EgoSystem { }

public abstract class EgoUpdateSystem< EI > : EgoUpdateSystem
    where EI : EgoInterface
{
    public abstract void Update( EI egoInterface );
}

public abstract class EgoUpdateSystem< EI, EC > : EgoUpdateSystem< EI >
    where EI : EgoInterface
    where EC : EgoConstraint, new()
{
    protected EC constraint;

    public EgoUpdateSystem()
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