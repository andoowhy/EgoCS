using UnityEngine;
using System.Collections.Generic;

public class EgoSystem<C1, C2> : EgoSystem 
    where C1 : Component
    where C2 : Component
{
    protected BitMask _mask = new BitMask( ComponentIDs.size );

    protected Dictionary<EgoComponent, EgoBundle<C1, C2>> _bundles = new Dictionary<EgoComponent, EgoBundle<C1, C2>>();
    public Dictionary<EgoComponent, EgoBundle<C1, C2>>.ValueCollection bundles { get { return _bundles.Values; } }

    public EgoSystem()
    {        
        _mask[ComponentIDs<C1>.ID] = true;
        _mask[ComponentIDs<C2>.ID] = true;
        _mask[ComponentIDs<EgoComponent>.ID] = true;

        // Attach built-in Event Handlers
        EgoEvents<AddedGameObject>.Add( Handle );
        EgoEvents<DestroyedGameObject>.Add( Handle );
        EgoEvents<AddedComponent<C1>>.Add( Handle );
        EgoEvents<AddedComponent<C2>>.Add( Handle );
        EgoEvents<DestroyedComponent<C1>>.Add( Handle );
        EgoEvents<DestroyedComponent<C2>>.Add( Handle );
    }

    public override void createBundles( EgoComponent[] egoComponents )
    {
        foreach( var egoComponent in egoComponents )
        {
            CreateBundle( egoComponent );
        }
    }

    protected void CreateBundle( EgoComponent egoComponent )
    {
        var andMask = new BitMask( egoComponent.mask ).And( _mask );
        if( andMask == _mask )
        {
            var component1 = egoComponent.GetComponent<C1>();
            var component2 = egoComponent.GetComponent<C2>();
            CreateBundle( egoComponent, component1, component2 );
        }
    }

    protected void CreateBundle( EgoComponent egoComponent, C1 component1, C2 component2 )
    {
        var bundle = new EgoBundle<C1, C2>( egoComponent.transform, component1, component2 );
        _bundles[egoComponent] = bundle;
    }

    protected void RemoveBundle( EgoComponent egoComponent )
    {
        var andMask = new BitMask( egoComponent.mask ).And( _mask );
        if( andMask != _mask )
        {
            _bundles.Remove( egoComponent );
        }
    }

    public override void Start() { }

    public override void Update() { }

    public override void FixedUpdate() { }

    //
    // Event Handlers
    //

    void Handle( AddedGameObject e )
    {
        CreateBundle( e.egoComponent );
    }

    void Handle( DestroyedGameObject e )
    {
        _bundles.Remove( e.egoComponent );
    }

    void Handle( AddedComponent<C1> e )
    {
        CreateBundle( e.egoComponent );
    }

    void Handle( AddedComponent<C2> e )
    {
        CreateBundle( e.egoComponent );
    }

    void Handle( DestroyedComponent<C1> e )
    {
        // Remove the component from the EgoComponent's mask
        e.egoComponent.mask[ComponentIDs<C1>.ID] = false;
        RemoveBundle( e.egoComponent );
    }

    void Handle( DestroyedComponent<C2> e )
    {
        // Remove the component from the EgoComponent's mask
        e.egoComponent.mask[ComponentIDs<C2>.ID] = false;
        RemoveBundle( e.egoComponent );
    }
}
