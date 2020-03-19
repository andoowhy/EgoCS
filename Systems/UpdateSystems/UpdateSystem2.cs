namespace EgoCS
{
    public abstract class UpdateSystem< TEgoInterface, TEgoConstraint1, TEgoConstraint2 > : UpdateSystem< TEgoInterface >
        where TEgoInterface : EgoCS, new()
        where TEgoConstraint1 : Constraint, new()
        where TEgoConstraint2 : Constraint, new()
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
}