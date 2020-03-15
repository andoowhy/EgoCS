public abstract class EgoUpdateSystem< TEgoInterface, TEgoConstraint1, TEgoConstraint2, TEgoConstraint3 > : EgoUpdateSystem< TEgoInterface >
    where TEgoInterface : EgoInterface
    where TEgoConstraint1 : EgoConstraint, new()
    where TEgoConstraint2 : EgoConstraint, new()
    where TEgoConstraint3 : EgoConstraint, new()

{
    private readonly TEgoConstraint1 constraint1;
    private readonly TEgoConstraint2 constraint2;
    private readonly TEgoConstraint3 constraint3;

    protected EgoUpdateSystem()
    {
        constraint1 = new TEgoConstraint1();
        constraint2 = new TEgoConstraint2();
        constraint3 = new TEgoConstraint3();

        EgoEvents< AddedGameObject >.AddHandler( e => constraint1.CreateBundles( e.egoComponent ) );
        EgoEvents< AddedGameObject >.AddHandler( e => constraint2.CreateBundles( e.egoComponent ) );
        EgoEvents< AddedGameObject >.AddHandler( e => constraint3.CreateBundles( e.egoComponent ) );

        EgoEvents< DestroyedGameObject >.AddHandler( e => constraint1.RemoveBundles( e.egoComponent ) );
        EgoEvents< DestroyedGameObject >.AddHandler( e => constraint2.RemoveBundles( e.egoComponent ) );
        EgoEvents< DestroyedGameObject >.AddHandler( e => constraint3.RemoveBundles( e.egoComponent ) );
    }

    public abstract void Update( TEgoInterface egoInterface, TEgoConstraint1 constraint1, TEgoConstraint2 constraint2, TEgoConstraint3 constraint3 );

    public override void Update( TEgoInterface egoInterface )
    {
        Update( egoInterface, constraint1, constraint2, constraint3 );
    }

    public override void CreateBundles( EgoComponent egoComponent )
    {
        constraint1.CreateBundles( egoComponent );
        constraint2.CreateBundles( egoComponent );
        constraint3.CreateBundles( egoComponent );
    }
}