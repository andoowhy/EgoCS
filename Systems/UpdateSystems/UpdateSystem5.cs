namespace EgoCS
{
    public abstract class UpdateSystem< TEgoInterface, TEgoConstraint1, TEgoConstraint2, TEgoConstraint3, TEgoConstraint4, TEgoConstraint5 > : UpdateSystem< TEgoInterface >
        where TEgoInterface : EgoCS
        where TEgoConstraint1 : Constraint, new()
        where TEgoConstraint2 : Constraint, new()
        where TEgoConstraint3 : Constraint, new()
        where TEgoConstraint4 : Constraint, new()
        where TEgoConstraint5 : Constraint, new()

    {
        private readonly TEgoConstraint1 constraint1 = new TEgoConstraint1();
        private readonly TEgoConstraint2 constraint2 = new TEgoConstraint2();
        private readonly TEgoConstraint3 constraint3 = new TEgoConstraint3();
        private readonly TEgoConstraint4 constraint4 = new TEgoConstraint4();
        private readonly TEgoConstraint5 constraint5 = new TEgoConstraint5();

        public abstract void Update( TEgoInterface egoInterface, TEgoConstraint1 constraint1, TEgoConstraint2 constraint2, TEgoConstraint3 constraint3, TEgoConstraint4 constraint4, TEgoConstraint5 constraint5 );

        public override void CreateConstraintCallbacks( TEgoInterface egoInterface )
        {
            egoInterface.AddAddedGameObjectCallback( constraint1.CreateBundles );
            egoInterface.AddAddedGameObjectCallback( constraint2.CreateBundles );
            egoInterface.AddAddedGameObjectCallback( constraint3.CreateBundles );
            egoInterface.AddAddedGameObjectCallback( constraint4.CreateBundles );
            egoInterface.AddAddedGameObjectCallback( constraint5.CreateBundles );

            egoInterface.AddDestroyedGameObjectCallback( constraint1.CreateBundles );
            egoInterface.AddDestroyedGameObjectCallback( constraint2.CreateBundles );
            egoInterface.AddDestroyedGameObjectCallback( constraint3.CreateBundles );
            egoInterface.AddDestroyedGameObjectCallback( constraint4.CreateBundles );
            egoInterface.AddDestroyedGameObjectCallback( constraint5.CreateBundles );

            constraint1.CreateConstraintCallbacks( egoInterface );
            constraint2.CreateConstraintCallbacks( egoInterface );
            constraint3.CreateConstraintCallbacks( egoInterface );
            constraint4.CreateConstraintCallbacks( egoInterface );
            constraint5.CreateConstraintCallbacks( egoInterface );
        }

        public override void Update( TEgoInterface egoInterface )
        {
            Update( egoInterface, constraint1, constraint2, constraint3, constraint4, constraint5 );
        }

        public override void CreateBundles( EgoComponent egoComponent )
        {
            constraint1.CreateBundles( egoComponent );
            constraint2.CreateBundles( egoComponent );
            constraint3.CreateBundles( egoComponent );
            constraint4.CreateBundles( egoComponent );
            constraint5.CreateBundles( egoComponent );
        }
    }
}