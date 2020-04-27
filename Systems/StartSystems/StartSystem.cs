namespace EgoCS
{
    public abstract class StartSystem< TEgoInterface > : System<TEgoInterface>
        where TEgoInterface : EgoCS
    {
        public abstract void Start( TEgoInterface egoInterface );
    }
}