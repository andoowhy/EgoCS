using System;

public abstract class EgoUpdateSystem< EI, TEgoConstraint1 > : EgoUpdateSystem< EI >
    where EI : EgoInterface, new()
    where TEgoConstraint1 : EgoConstraint, new()
{
    protected TEgoConstraint1 constraint1;

    protected EgoUpdateSystem()
    {
        constraint1 = new TEgoConstraint1();

        EgoEvents< AddedGameObject >.AddHandler( e => constraint1.CreateBundles( e.egoComponent ) );

        EgoEvents< DestroyedGameObject >.AddHandler( e => constraint1.RemoveBundles( e.egoComponent ) );
    }

    public abstract void Update( EI egoInterface, TEgoConstraint1 egoConstraint1 );

    public override void Update( EI egoInterface )
    {
        Update( egoInterface, constraint1 );
    }

    public override void CreateBundles( EgoComponent egoComponent )
    {
        constraint1.CreateBundles( egoComponent );
    }
}