namespace EgoCS
{
    public abstract class EgoFixedUpdateSystem< TEgoInterface > : EgoSystem< TEgoInterface >
        where TEgoInterface : EgoCS
    {
        public abstract void FixedUpdate( TEgoInterface egoInterface );
    }
}