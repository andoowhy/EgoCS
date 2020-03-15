public abstract class EgoStartSystem<TEgoInterface, TEgoConstraint1, TEgoConstraint2> : EgoStartSystem<TEgoInterface>
    where TEgoInterface : EgoInterface
    where TEgoConstraint1 : EgoConstraint, new()
    where TEgoConstraint2 : EgoConstraint, new()

{
    private readonly TEgoConstraint1 constraint1;
    private readonly TEgoConstraint2 constraint2;

    protected EgoStartSystem()
    {
        constraint1 = new TEgoConstraint1();
        constraint2 = new TEgoConstraint2();

        EgoEvents<AddedGameObject>.AddHandler( e => constraint1.CreateBundles( e.egoComponent ) );
        EgoEvents<AddedGameObject>.AddHandler( e => constraint2.CreateBundles( e.egoComponent ) );

        EgoEvents<DestroyedGameObject>.AddHandler( e => constraint1.RemoveBundles( e.egoComponent ) );
        EgoEvents<DestroyedGameObject>.AddHandler( e => constraint2.RemoveBundles( e.egoComponent ) );

    }

    public abstract void Start( TEgoInterface egoInterface, TEgoConstraint1 constraint1, TEgoConstraint2 constraint2 );

    public override void Start( TEgoInterface egoInterface )
    {
        Start( egoInterface, constraint1, constraint2 );
    }

    public override void CreateBundles( EgoComponent egoComponent )
    {
        constraint1.CreateBundles( egoComponent );
        constraint2.CreateBundles( egoComponent );

    }
}