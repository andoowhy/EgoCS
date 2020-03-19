namespace EgoCS
{
    public abstract class StartSystem : EgoSystem { }

    public abstract class StartSystem< TEgoInterface > : StartSystem
        where TEgoInterface : EgoCS
    {
        public abstract void Start( TEgoInterface egoInterface );
        public abstract void CreateConstraintCallbacks( TEgoInterface egoInterface );
    }
}