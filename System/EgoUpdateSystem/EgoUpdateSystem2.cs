using System;

public abstract class EgoUpdateSystem< TEgoInterface, TEgoConstraint1, TEgoConstraint2 > : EgoUpdateSystem< TEgoInterface >
    where TEgoInterface : EgoInterface, new()
    where TEgoConstraint1 : EgoConstraint, new()
    where TEgoConstraint2 : EgoConstraint, new()
{
    private readonly TEgoConstraint1 constraint1 = new TEgoConstraint1();
    private readonly TEgoConstraint2 constraint2 = new TEgoConstraint2();

    public abstract void Update( TEgoInterface egoInterface, TEgoConstraint1 constraint1, TEgoConstraint2 constraint2 );

    public override void CreateConstraintCallbacks( TEgoInterface egoInterface )
    {
        egoInterface.AddAddedGameObjectCallback( constraint1.CreateBundles );
        egoInterface.AddAddedGameObjectCallback( constraint2.CreateBundles );

        egoInterface.AddDestroyedGameObjectCallback( constraint1.RemoveBundles );
        egoInterface.AddDestroyedGameObjectCallback( constraint2.RemoveBundles );

        constraint1.CreateConstraintCallbacks( egoInterface );
        constraint2.CreateConstraintCallbacks( egoInterface );
    }

    public override void CreateBundles( EgoComponent egoComponent )
    {
        constraint1.CreateBundles( egoComponent );
    }

    public override void Update( TEgoInterface egoInterface )
    {
        Update(
            egoInterface,
            constraint1,
            constraint2
        );
    }
}