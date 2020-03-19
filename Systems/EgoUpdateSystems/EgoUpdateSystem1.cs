using System;

public abstract class EgoUpdateSystem< TEgoInterface, TEgoConstraint1 > : EgoUpdateSystem< TEgoInterface >
    where TEgoInterface : EgoCS, new()
    where TEgoConstraint1 : EgoConstraint, new()
{
    private readonly TEgoConstraint1 constraint1 = new TEgoConstraint1();

    public abstract void Update( TEgoInterface egoInterface, TEgoConstraint1 egoConstraint1 );

    public override void CreateConstraintCallbacks( TEgoInterface egoInterface )
    {
        egoInterface.AddAddedGameObjectCallback( constraint1.CreateBundles );

        egoInterface.AddDestroyedGameObjectCallback( constraint1.RemoveBundles );

        constraint1.CreateConstraintCallbacks( egoInterface );
    }

    public override void Update( TEgoInterface egoInterface )
    {
        Update( egoInterface, constraint1 );
    }

    public override void CreateBundles( EgoComponent egoComponent )
    {
        constraint1.CreateBundles( egoComponent );
    }
}