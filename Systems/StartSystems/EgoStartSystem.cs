namespace EgoCS
{
    public abstract class EgoStartSystem : EgoSystem { }

    public abstract class EgoStartSystem< TEgoInterface > : EgoStartSystem
        where TEgoInterface : EgoCS
    {
        public abstract void Start( TEgoInterface egoInterface );
        public abstract void CreateConstraintCallbacks( TEgoInterface egoInterface );
    }
}