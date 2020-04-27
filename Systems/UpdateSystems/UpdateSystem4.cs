namespace EgoCS
{
    public abstract class UpdateSystem< TEgoInterface, TEgoConstraint1, TEgoConstraint2, TEgoConstraint3, TEgoConstraint4 > : UpdateSystem< TEgoInterface >
        where TEgoInterface : EgoCS
        where TEgoConstraint1 : Constraint, new()
        where TEgoConstraint2 : Constraint, new()
        where TEgoConstraint3 : Constraint, new()
        where TEgoConstraint4 : Constraint, new()

    {
        private readonly TEgoConstraint1 constraint1 = new TEgoConstraint1();
        private readonly TEgoConstraint2 constraint2 = new TEgoConstraint2();
        private readonly TEgoConstraint3 constraint3 = new TEgoConstraint3();
        private readonly TEgoConstraint4 constraint4 = new TEgoConstraint4();

        public abstract void Update( TEgoInterface egoInterface, TEgoConstraint1 constraint1, TEgoConstraint2 constraint2, TEgoConstraint3 constraint3, TEgoConstraint4 constraint4 );

        public override void InitConstraints( BitMaskPool bitMaskPool )
        {
            constraint1.CreateMask( bitMaskPool );
            constraint2.CreateMask( bitMaskPool );
            constraint3.CreateMask( bitMaskPool );
            constraint4.CreateMask( bitMaskPool );

            constraint1.InitMask();
            constraint2.InitMask();
            constraint3.InitMask();
            constraint4.InitMask();
        }

        public override void CreateConstraintCallbacks( TEgoInterface egoInterface )
        {
            egoInterface.AddAddedGameObjectCallback( constraint1.CreateBundles );
            egoInterface.AddAddedGameObjectCallback( constraint2.CreateBundles );
            egoInterface.AddAddedGameObjectCallback( constraint3.CreateBundles );
            egoInterface.AddAddedGameObjectCallback( constraint4.CreateBundles );

            egoInterface.AddDestroyedGameObjectCallback( constraint1.RemoveBundles );
            egoInterface.AddDestroyedGameObjectCallback( constraint2.RemoveBundles );
            egoInterface.AddDestroyedGameObjectCallback( constraint3.RemoveBundles );
            egoInterface.AddDestroyedGameObjectCallback( constraint4.RemoveBundles );

            constraint1.CreateConstraintCallbacks( egoInterface );
            constraint2.CreateConstraintCallbacks( egoInterface );
            constraint3.CreateConstraintCallbacks( egoInterface );
            constraint4.CreateConstraintCallbacks( egoInterface );
        }

        public override void Update( TEgoInterface egoInterface )
        {
            Update( egoInterface, constraint1, constraint2, constraint3, constraint4 );
        }

        public override void CreateBundles( EgoComponent egoComponent, BitMaskPool bitMaskPool )
        {
            constraint1.CreateBundles( egoComponent, bitMaskPool );
            constraint2.CreateBundles( egoComponent, bitMaskPool );
            constraint3.CreateBundles( egoComponent, bitMaskPool );
            constraint4.CreateBundles( egoComponent, bitMaskPool );
        }
    }
}