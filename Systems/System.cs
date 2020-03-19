namespace EgoCS
{
    public abstract class EgoSystem
    {
#if UNITY_EDITOR
        public bool enabled = true;
#endif

        public abstract void CreateBundles( EgoComponent egoComponents );
    }

    public abstract class EgoSystem< TEgoInterface > : EgoSystem
        where TEgoInterface : EgoCS
    {
        public abstract void CreateConstraintCallbacks( TEgoInterface egoInterface );
    }
}