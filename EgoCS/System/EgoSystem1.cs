using UnityEngine;
using System.Collections.Generic;

public class EgoSytem<C1> : EgoSystem
    where C1 : Component
{
    protected BitMask _mask = new BitMask( ComponentIDs.size );

    protected List<EgoBundle<C1>> _bundles = new List<EgoBundle<C1>>( EgoSystem.DEFAULT_CAPACITY );
    protected Dictionary< EgoComponent, int > _bundlesLookup = new Dictionary<EgoComponent, int>( EgoSystem.DEFAULT_CAPACITY );
    public List<EgoBundle<C1>> bundles { get { return _bundles; } }

    public EgoSytem()
    {        
        _mask[ComponentIDs<C1>.ID] = true;
        _mask[ComponentIDs<EgoComponent>.ID] = true;

        // Attach built-in Event Handlers
        EgoEvents<AddedGameObject>.Add( Handle );
        EgoEvents<DestroyedGameObject>.Add( Handle );
        EgoEvents<AddedComponent<C1>>.Add( Handle );
        EgoEvents<DestroyedComponent<C1>>.Add( Handle );
    }

    public override void createBundles( EgoComponent[] egoComponents )
    {
        foreach( var EgoComponent in egoComponents )
        {
            CreateBundle( EgoComponent );
        }
    }

    protected void CreateBundle( EgoComponent EgoComponent )
    {
        var andMask = new BitMask( EgoComponent.mask ).And( _mask );
        if( andMask == _mask )
        {
            var component1 = EgoComponent.GetComponent<C1>();
            CreateBundle( EgoComponent, component1 );
        }
    }

    protected void CreateBundle( EgoComponent EgoComponent, C1 component1 )
    {
        EgoBundle<C1> bundle = new EgoBundle<C1>( EgoComponent.transform, component1 );
        _bundles.Add( bundle );
        _bundlesLookup[EgoComponent] = _bundles.Count - 1;
    }

    protected void RemoveBundle( EgoComponent EgoComponent )
    {
        int index;
        if( _bundlesLookup.TryGetValue( EgoComponent, out index ) )
        {
            var temp = _bundles[index];
            _bundles[index] = _bundles[_bundles.Count - 1];
            _bundles[_bundles.Count - 1] = temp;
            _bundles.RemoveAt( _bundles.Count - 1 );
        }
    }

    public override void Start() { }

    public override void Update() { }

    //
    // Event Handlers
    //

    void Handle( AddedGameObject e )
    {
        CreateBundle( e.egoComponent );
    }

    void Handle( DestroyedGameObject e )
    {
        RemoveBundle( e.egoComponent );
    }

    void Handle( AddedComponent<C1> e )
    {
        CreateBundle( e.egoComponent );
    }

    void Handle( DestroyedComponent<C1> e )
    {
        // Remove the component from the EgoComponent's mask
        e.egoComponent.mask[ComponentIDs<C1>.ID] = false;
        RemoveBundle( e.egoComponent );
    }
}
