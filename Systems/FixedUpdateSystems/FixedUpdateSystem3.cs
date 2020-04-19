namespace EgoCS
{
    public abstract class FixedUpdateSystem< TEgoInterface, TEgoConstraint1, TEgoConstraint2, TEgoConstraint3 > : FixedUpdateSystem< TEgoInterface >
        where TEgoInterface : EgoCS
        where TEgoConstraint1 : Constraint, new()
        where TEgoConstraint2 : Constraint, new()
        where TEgoConstraint3 : Constraint, new()

    {
        private readonly TEgoConstraint1 constraint1 = new TEgoConstraint1();
        private readonly TEgoConstraint2 constraint2 = new TEgoConstraint2();
        private readonly TEgoConstraint3 constraint3 = new TEgoConstraint3();

        public abstract void FixedUpdate( TEgoInterface egoInterface, TEgoConstraint1 constraint1, TEgoConstraint2 constraint2, TEgoConstraint3 constraint3 );

        public override void FixedUpdate( TEgoInterface egoInterface )
        {
            FixedUpdate( egoInterface, constraint1, constraint2, constraint3 );
        }

        public override void CreateConstraintCallbacks( TEgoInterface egoInterface )
        {
            egoInterface.AddAddedGameObjectCallback( constraint1.CreateBundles );
            egoInterface.AddAddedGameObjectCallback( constraint2.CreateBundles );
            egoInterface.AddAddedGameObjectCallback( constraint3.CreateBundles );

            egoInterface.AddDestroyedGameObjectCallback( constraint1.CreateBundles );
            egoInterface.AddDestroyedGameObjectCallback( constraint2.CreateBundles );
            egoInterface.AddDestroyedGameObjectCallback( constraint3.CreateBundles );

            constraint1.CreateConstraintCallbacks( egoInterface );
            constraint2.CreateConstraintCallbacks( egoInterface );
            constraint3.CreateConstraintCallbacks( egoInterface );
        }

        public override void CreateBundles( EgoComponent egoComponent )
        {
            constraint1.CreateBundles( egoComponent );
            constraint2.CreateBundles( egoComponent );
            constraint3.CreateBundles( egoComponent );
        }
    }
}