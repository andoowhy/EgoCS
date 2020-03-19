namespace EgoCS
{
    public abstract class EgoFixedUpdateSystem< TEgoInterface, TEgoConstraint1, TEgoConstraint2 > : EgoFixedUpdateSystem< TEgoInterface >
        where TEgoInterface : EgoCS
        where TEgoConstraint1 : Constraint, new()
        where TEgoConstraint2 : Constraint, new()
    {
        private readonly TEgoConstraint1 constraint1 = new TEgoConstraint1();
        private readonly TEgoConstraint2 constraint2 = new TEgoConstraint2();

        public abstract void FixedUpdate( TEgoInterface egoInterface, TEgoConstraint1 constraint1, TEgoConstraint2 constraint2 );

        public override void CreateConstraintCallbacks( TEgoInterface egoInterface )
        {
            egoInterface.AddAddedGameObjectCallback( constraint1.CreateBundles );
            egoInterface.AddAddedGameObjectCallback( constraint2.CreateBundles );

            egoInterface.AddAddedGameObjectCallback( constraint1.RemoveBundles );
            egoInterface.AddAddedGameObjectCallback( constraint2.RemoveBundles );

            constraint1.CreateConstraintCallbacks( egoInterface );
            constraint2.CreateConstraintCallbacks( egoInterface );
        }

        public override void CreateBundles( EgoComponent egoComponent )
        {
            constraint1.CreateBundles( egoComponent );
        }

        public override void FixedUpdate( TEgoInterface egoInterface )
        {
            FixedUpdate(
                egoInterface,
                constraint1,
                constraint2
            );
        }
    }
}