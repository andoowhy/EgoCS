namespace EgoCS
{
    public abstract class System
    {
#if UNITY_EDITOR
        public bool enabled = true;
#endif

        public abstract void CreateBundles( EgoComponent egoComponents );
    }

    public abstract class System< TEgoInterface > : System
        where TEgoInterface : EgoCS
    {
        public abstract void CreateConstraintCallbacks( TEgoInterface egoInterface );
    }
}