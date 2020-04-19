namespace EgoCS
{
    public abstract class FixedUpdateSystem< TEgoInterface > : System< TEgoInterface >
        where TEgoInterface : EgoCS
    {
        public abstract void FixedUpdate( TEgoInterface egoInterface );
    }
}