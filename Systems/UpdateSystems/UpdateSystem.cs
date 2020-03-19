namespace EgoCS
{
    public abstract class UpdateSystem< TEgoInterface > : EgoSystem< TEgoInterface >
        where TEgoInterface : EgoCS
    {
        public abstract void Update( TEgoInterface egoInterface );
    }
}