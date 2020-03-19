public abstract class EgoStartSystem< TEgoInterface, TEgoConstraint1, TEgoConstraint2, TEgoConstraint3, TEgoConstraint4, TEgoConstraint5, TEgoConstraint6, TEgoConstraint7 > : EgoStartSystem< TEgoInterface >
    where TEgoInterface : EgoCS
    where TEgoConstraint1 : EgoConstraint, new()
    where TEgoConstraint2 : EgoConstraint, new()
    where TEgoConstraint3 : EgoConstraint, new()
    where TEgoConstraint4 : EgoConstraint, new()
    where TEgoConstraint5 : EgoConstraint, new()
    where TEgoConstraint6 : EgoConstraint, new()
    where TEgoConstraint7 : EgoConstraint, new()

{
    private readonly TEgoConstraint1 constraint1 = new TEgoConstraint1();
    private readonly TEgoConstraint2 constraint2 = new TEgoConstraint2();
    private readonly TEgoConstraint3 constraint3 = new TEgoConstraint3();
    private readonly TEgoConstraint4 constraint4 = new TEgoConstraint4();
    private readonly TEgoConstraint5 constraint5 = new TEgoConstraint5();
    private readonly TEgoConstraint6 constraint6 = new TEgoConstraint6();
    private readonly TEgoConstraint7 constraint7 = new TEgoConstraint7();

    public abstract void Start( TEgoInterface egoInterface, TEgoConstraint1 constraint1, TEgoConstraint2 constraint2, TEgoConstraint3 constraint3, TEgoConstraint4 constraint4, TEgoConstraint5 constraint5, TEgoConstraint6 constraint6, TEgoConstraint7 constraint7 );

    public override void CreateConstraintCallbacks( TEgoInterface egoInterface )
    {
        egoInterface.AddAddedGameObjectCallback( constraint1.CreateBundles );
        egoInterface.AddAddedGameObjectCallback( constraint2.CreateBundles );
        egoInterface.AddAddedGameObjectCallback( constraint3.CreateBundles );
        egoInterface.AddAddedGameObjectCallback( constraint4.CreateBundles );
        egoInterface.AddAddedGameObjectCallback( constraint5.CreateBundles );
        egoInterface.AddAddedGameObjectCallback( constraint6.CreateBundles );
        egoInterface.AddAddedGameObjectCallback( constraint7.CreateBundles );

        egoInterface.AddDestroyedGameObjectCallback( constraint1.RemoveBundles );
        egoInterface.AddDestroyedGameObjectCallback( constraint2.RemoveBundles );
        egoInterface.AddDestroyedGameObjectCallback( constraint3.RemoveBundles );
        egoInterface.AddDestroyedGameObjectCallback( constraint4.RemoveBundles );
        egoInterface.AddDestroyedGameObjectCallback( constraint5.RemoveBundles );
        egoInterface.AddDestroyedGameObjectCallback( constraint6.RemoveBundles );
        egoInterface.AddDestroyedGameObjectCallback( constraint7.RemoveBundles );

        constraint1.CreateConstraintCallbacks( egoInterface );
        constraint2.CreateConstraintCallbacks( egoInterface );
        constraint3.CreateConstraintCallbacks( egoInterface );
        constraint4.CreateConstraintCallbacks( egoInterface );
        constraint5.CreateConstraintCallbacks( egoInterface );
        constraint6.CreateConstraintCallbacks( egoInterface );
        constraint7.CreateConstraintCallbacks( egoInterface );
    }

    public override void Start( TEgoInterface egoInterface )
    {
        Start( egoInterface, constraint1, constraint2, constraint3, constraint4, constraint5, constraint6, constraint7 );
    }

    public override void CreateBundles( EgoComponent egoComponent )
    {
        constraint1.CreateBundles( egoComponent );
        constraint2.CreateBundles( egoComponent );
        constraint3.CreateBundles( egoComponent );
        constraint4.CreateBundles( egoComponent );
        constraint5.CreateBundles( egoComponent );
        constraint6.CreateBundles( egoComponent );
        constraint7.CreateBundles( egoComponent );
    }
}