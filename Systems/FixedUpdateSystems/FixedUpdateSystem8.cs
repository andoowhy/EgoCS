using NotImplementedException = System.NotImplementedException;

namespace EgoCS
{
    public abstract class FixedUpdateSystem< TEgoInterface, TEgoConstraint1, TEgoConstraint2, TEgoConstraint3, TEgoConstraint4, TEgoConstraint5, TEgoConstraint6, TEgoConstraint7, TEgoConstraint8 > : FixedUpdateSystem< TEgoInterface >
        where TEgoInterface : EgoCS
        where TEgoConstraint1 : Constraint, new()
        where TEgoConstraint2 : Constraint, new()
        where TEgoConstraint3 : Constraint, new()
        where TEgoConstraint4 : Constraint, new()
        where TEgoConstraint5 : Constraint, new()
        where TEgoConstraint6 : Constraint, new()
        where TEgoConstraint7 : Constraint, new()
        where TEgoConstraint8 : Constraint, new()

    {
        private readonly TEgoConstraint1 constraint1 = new TEgoConstraint1();
        private readonly TEgoConstraint2 constraint2 = new TEgoConstraint2();
        private readonly TEgoConstraint3 constraint3 = new TEgoConstraint3();
        private readonly TEgoConstraint4 constraint4 = new TEgoConstraint4();
        private readonly TEgoConstraint5 constraint5 = new TEgoConstraint5();
        private readonly TEgoConstraint6 constraint6 = new TEgoConstraint6();
        private readonly TEgoConstraint7 constraint7 = new TEgoConstraint7();
        private readonly TEgoConstraint8 constraint8 = new TEgoConstraint8();

        public abstract void FixedUpdate( TEgoInterface egoInterface, TEgoConstraint1 constraint1, TEgoConstraint2 constraint2, TEgoConstraint3 constraint3, TEgoConstraint4 constraint4, TEgoConstraint5 constraint5, TEgoConstraint6 constraint6, TEgoConstraint7 constraint7, TEgoConstraint8 constraint8 );

        public override void InitConstraints( BitMaskPool bitMaskPool )
        {
            constraint1.CreateMask( bitMaskPool );
            constraint2.CreateMask( bitMaskPool );
            constraint3.CreateMask( bitMaskPool );
            constraint4.CreateMask( bitMaskPool );
            constraint5.CreateMask( bitMaskPool );
            constraint6.CreateMask( bitMaskPool );
            constraint7.CreateMask( bitMaskPool );
            constraint8.CreateMask( bitMaskPool );

            constraint1.InitMask();
            constraint2.InitMask();
            constraint3.InitMask();
            constraint4.InitMask();
            constraint5.InitMask();
            constraint6.InitMask();
            constraint7.InitMask();
            constraint8.InitMask();
        }

        public override void CreateConstraintCallbacks( TEgoInterface egoInterface )
        {
            egoInterface.AddAddedGameObjectCallback( constraint1.CreateBundles );
            egoInterface.AddAddedGameObjectCallback( constraint2.CreateBundles );
            egoInterface.AddAddedGameObjectCallback( constraint3.CreateBundles );
            egoInterface.AddAddedGameObjectCallback( constraint4.CreateBundles );
            egoInterface.AddAddedGameObjectCallback( constraint5.CreateBundles );
            egoInterface.AddAddedGameObjectCallback( constraint6.CreateBundles );
            egoInterface.AddAddedGameObjectCallback( constraint7.CreateBundles );
            egoInterface.AddAddedGameObjectCallback( constraint8.CreateBundles );

            egoInterface.AddDestroyedGameObjectCallback( constraint1.RemoveBundles );
            egoInterface.AddDestroyedGameObjectCallback( constraint2.RemoveBundles );
            egoInterface.AddDestroyedGameObjectCallback( constraint3.RemoveBundles );
            egoInterface.AddDestroyedGameObjectCallback( constraint4.RemoveBundles );
            egoInterface.AddDestroyedGameObjectCallback( constraint5.RemoveBundles );
            egoInterface.AddDestroyedGameObjectCallback( constraint6.RemoveBundles );
            egoInterface.AddDestroyedGameObjectCallback( constraint7.RemoveBundles );
            egoInterface.AddDestroyedGameObjectCallback( constraint8.RemoveBundles );

            constraint1.CreateConstraintCallbacks( egoInterface );
            constraint2.CreateConstraintCallbacks( egoInterface );
            constraint3.CreateConstraintCallbacks( egoInterface );
            constraint4.CreateConstraintCallbacks( egoInterface );
            constraint5.CreateConstraintCallbacks( egoInterface );
            constraint6.CreateConstraintCallbacks( egoInterface );
            constraint7.CreateConstraintCallbacks( egoInterface );
            constraint8.CreateConstraintCallbacks( egoInterface );
        }

        public override void FixedUpdate( TEgoInterface egoInterface )
        {
            FixedUpdate( egoInterface, constraint1, constraint2, constraint3, constraint4, constraint5, constraint6, constraint7, constraint8 );
        }

        public override void CreateBundles( EgoComponent egoComponent, BitMaskPool bitMaskPool )
        {
            constraint1.CreateBundles( egoComponent, bitMaskPool );
            constraint2.CreateBundles( egoComponent, bitMaskPool );
            constraint3.CreateBundles( egoComponent, bitMaskPool );
            constraint4.CreateBundles( egoComponent, bitMaskPool );
            constraint5.CreateBundles( egoComponent, bitMaskPool );
            constraint6.CreateBundles( egoComponent, bitMaskPool );
            constraint7.CreateBundles( egoComponent, bitMaskPool );
            constraint8.CreateBundles( egoComponent, bitMaskPool );
        }
    }
}