using System;

public abstract class EgoUpdateSystem< EI, C1, C2> : EgoUpdateSystem< EI >
    where EI : EgoInterface, new()
    where C1 : EgoConstraint, new()
    where C2 : EgoConstraint, new()
{
    protected C1 constraint1;
    protected C2 constraint2;

    public EgoUpdateSystem()
    {
        constraint1 = new C1();
        constraint2 = new C2();
        EgoEvents< AddedGameObject >.AddHandler( e => constraint1.CreateBundles( e.egoComponent ) );
        EgoEvents< AddedGameObject >.AddHandler( e => constraint2.CreateBundles( e.egoComponent ) );
        EgoEvents< DestroyedGameObject >.AddHandler( e => constraint1.RemoveBundles( e.egoComponent ) );
        EgoEvents< DestroyedGameObject >.AddHandler( e => constraint2.RemoveBundles( e.egoComponent ) );
    }

    public override void CreateBundles( EgoComponent egoComponent )
    {
        constraint1.CreateBundles( egoComponent );
    }
}