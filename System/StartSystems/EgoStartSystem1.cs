public abstract class EgoStartSystem<TEgoInterface, TEgoConstraint1> : EgoStartSystem<TEgoInterface>
    where TEgoInterface : EgoInterface
    where TEgoConstraint1 : EgoConstraint, new()

{
    private readonly TEgoConstraint1 constraint1;

    protected EgoStartSystem()
    {
        constraint1 = new TEgoConstraint1();

        EgoEvents<AddedGameObject>.AddHandler( e => constraint1.CreateBundles( e.egoComponent ) );

        EgoEvents<DestroyedGameObject>.AddHandler( e => constraint1.RemoveBundles( e.egoComponent ) );

    }

    public abstract void Start( TEgoInterface egoInterface, TEgoConstraint1 constraint1 );

    public override void Start( TEgoInterface egoInterface )
    {
        Start( egoInterface, constraint1 );
    }

    public override void CreateBundles( EgoComponent egoComponent )
    {
        constraint1.CreateBundles( egoComponent );

    }
}