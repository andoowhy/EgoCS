namespace EgoCS
{
    public abstract class EgoUpdateSystem< TEgoInterface > : EgoSystem< TEgoInterface >
        where TEgoInterface : EgoCS
    {
        public abstract void Update( TEgoInterface egoInterface );
    }
}