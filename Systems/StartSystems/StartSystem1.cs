namespace EgoCS
{
    public abstract class StartSystem< TEgoInterface, TEgoConstraint1 > : StartSystem< TEgoInterface >
        where TEgoInterface : EgoCS
        where TEgoConstraint1 : Constraint, new()

    {
        private readonly TEgoConstraint1 constraint1 = new TEgoConstraint1();

        public abstract void Start( TEgoInterface egoInterface, TEgoConstraint1 constraint1 );

        public override void CreateConstraintCallbacks( TEgoInterface egoInterface )
        {
            egoInterface.AddAddedGameObjectCallback( constraint1.CreateBundles );

            egoInterface.AddDestroyedGameObjectCallback( constraint1.RemoveBundles );

            constraint1.CreateConstraintCallbacks( egoInterface );
        }

        public override void Start( TEgoInterface egoInterface )
        {
            Start( egoInterface, constraint1 );
        }

        public override void CreateBundles( EgoComponent egoComponent )
        {
            constraint1.CreateBundles( egoComponent );
        }
    }
}