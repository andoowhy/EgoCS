public abstract class EgoSystem
{
    public const int DEFAULT_CAPACITY = 128;

    public abstract void createBundles( EgoComponent[] egoComponents );
    public abstract void Start();
    public abstract void Update();
    public abstract void FixedUpdate();
}