using System;

public abstract class EgoFixedUpdateSystem< EI, TEgoConstraint1 > : EgoFixedUpdateSystem< EI >
    where EI : EgoInterface, new()
    where TEgoConstraint1 : EgoConstraint, new()
{
    protected TEgoConstraint1 constraint1;

    protected EgoFixedUpdateSystem()
    {
        constraint1 = new TEgoConstraint1();

        EgoEvents< AddedGameObject >.AddHandler( e => constraint1.CreateBundles( e.egoComponent ) );

        EgoEvents< DestroyedGameObject >.AddHandler( e => constraint1.RemoveBundles( e.egoComponent ) );
    }

    public abstract void FixedUpdate( EI egoInterface, TEgoConstraint1 egoConstraint1 );

    public override void FixedUpdate( EI egoInterface )
    {
        FixedUpdate( egoInterface, constraint1 );
    }

    public override void CreateBundles( EgoComponent egoComponent )
    {
        constraint1.CreateBundles( egoComponent );
    }
}