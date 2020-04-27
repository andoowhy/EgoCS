namespace EgoCS
{
    public abstract class System
    {
#if UNITY_EDITOR
        public bool enabled = true;
#endif

        public abstract void CreateBundles( EgoComponent egoComponents, BitMaskPool bitMaskPool );
    }

    public abstract class System< TEgoInterface > : System
        where TEgoInterface : EgoCS
    {
        public abstract void InitConstraints( BitMaskPool bitMaskPool );
        public abstract void CreateConstraintCallbacks( TEgoInterface egoInterface );
    }
}