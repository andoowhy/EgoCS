using UnityEngine;
using System.Collections.Generic;

public class EgoSystem<C1, C2> : EgoSystem
    where C1 : Component
    where C2 : Component
{
    protected Dictionary<EgoComponent, EgoBundle<C1, C2>> _bundles = new Dictionary<EgoComponent, EgoBundle<C1, C2>>();
    
    protected delegate void ForEachGameObjectDelegate( EgoComponent egoComponent, C1 component1, C2 component2 );

    public EgoSystem()
    {
        _mask[ComponentIDs.Get( typeof( C1 ) )] = true;
        _mask[ComponentIDs.Get( typeof( C2 ) )] = true;
        _mask[ComponentIDs.Get( typeof( EgoComponent ) )] = true;

        // Attach built-in Event Handlers
        EgoEvents<AddedGameObject>.AddHandler( Handle );
        EgoEvents<DestroyedGameObject>.AddHandler( Handle );
        EgoEvents<AddedComponent<C1>>.AddHandler( Handle );
        EgoEvents<AddedComponent<C2>>.AddHandler( Handle );
        EgoEvents<DestroyedComponent<C1>>.AddHandler( Handle );
        EgoEvents<DestroyedComponent<C2>>.AddHandler( Handle );
    }

    public override void CreateBundles( EgoComponent[] egoComponents )
    {
        foreach( var egoComponent in egoComponents )
        {
            CreateBundle( egoComponent );
        }
    }

    protected void CreateBundle( EgoComponent egoComponent )
    {
        if( Ego.CanUpdate( _mask, egoComponent.mask ) )
        {
            var component1 = egoComponent.GetComponent<C1>();
            var component2 = egoComponent.GetComponent<C2>();
            CreateBundle( egoComponent, component1, component2 );
        }
    }

    protected void CreateBundle( EgoComponent egoComponent, C1 component1 )
    {
        if( Ego.CanUpdate( _mask, egoComponent.mask ) )
        {
            var component2 = egoComponent.GetComponent<C2>();
            CreateBundle( egoComponent, component1, component2 );
        }
    }

    protected void CreateBundle( EgoComponent egoComponent, C2 component2 )
    {
        if( Ego.CanUpdate( _mask, egoComponent.mask ) )
        {
            var component1 = egoComponent.GetComponent<C1>();
            CreateBundle( egoComponent, component1, component2 );
        }
    }

    protected void CreateBundle( EgoComponent egoComponent, C1 component1, C2 component2 )
    {
        var bundle = new EgoBundle<C1, C2>( egoComponent, component1, component2 );
        _bundles[egoComponent] = bundle;
    }

    protected void RemoveBundle( EgoComponent egoComponent )
    {
        _bundles.Remove( egoComponent );
    }

    protected void ForEachGameObject( ForEachGameObjectDelegate callback )
    {
        foreach( var bundle in _bundles.Values )
        {
            callback( bundle.egoComponent, bundle.component1, bundle.component2 );
        }
    }

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
        CreateBundle( e.egoComponent, e.component );
    }

    void Handle( AddedComponent<C2> e )
    {
        CreateBundle( e.egoComponent, e.component );
    }

    void Handle( DestroyedComponent<C1> e )
    {
        RemoveBundle( e.egoComponent );
    }

    void Handle( DestroyedComponent<C2> e )
    {
        RemoveBundle( e.egoComponent );
    }
}
