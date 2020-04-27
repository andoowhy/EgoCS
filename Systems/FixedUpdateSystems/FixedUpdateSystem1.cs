namespace EgoCS
{
    public abstract class FixedUpdateSystem< TEgoInterface, TEgoConstraint1 > : FixedUpdateSystem< TEgoInterface >
        where TEgoInterface : EgoCS, new()
        where TEgoConstraint1 : Constraint, new()
    {
        private readonly TEgoConstraint1 constraint1 = new TEgoConstraint1();

        public abstract void FixedUpdate( TEgoInterface egoInterface, TEgoConstraint1 egoConstraint1 );

        public override void FixedUpdate( TEgoInterface egoInterface )
        {
            FixedUpdate( egoInterface, constraint1 );
        }

        public override void InitConstraints( BitMaskPool bitMaskPool )
        {
            constraint1.CreateMask( bitMaskPool );

            constraint1.InitMask();
        }

        public override void CreateConstraintCallbacks( TEgoInterface egoInterface )
        {
            constraint1.CreateMask( egoInterface.bitMaskPool );

            egoInterface.AddAddedGameObjectCallback( constraint1.CreateBundles );

            egoInterface.AddDestroyedGameObjectCallback( constraint1.RemoveBundles );

            constraint1.CreateConstraintCallbacks( egoInterface );
        }

        public override void CreateBundles( EgoComponent egoComponent, BitMaskPool bitMaskPool )
        {
            constraint1.CreateBundles( egoComponent, bitMaskPool );
        }
    }
}