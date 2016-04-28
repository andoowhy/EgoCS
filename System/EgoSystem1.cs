using UnityEngine;
using System.Collections.Generic;

public class EgoSystem<C1> : EgoSystem
    where C1 : Component
{
    protected Dictionary<EgoComponent, EgoBundle<C1>> _bundles = new Dictionary<EgoComponent, EgoBundle<C1>>();

    protected delegate void ForEachGameObjectDelegate( EgoComponent egoComponent, C1 component1 );

    public EgoSystem()
    {
        _mask[ComponentIDs.Get( typeof( C1 ) )] = true;
        _mask[ComponentIDs.Get( typeof( EgoComponent ) )] = true;

        // Attach built-in Event Handlers
        EgoEvents<AddedGameObject>.AddHandler( Handle );
        EgoEvents<DestroyedGameObject>.AddHandler( Handle );
        EgoEvents<AddedComponent<C1>>.AddHandler( Handle );
        EgoEvents<DestroyedComponent<C1>>.AddHandler( Handle );
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
            CreateBundle( egoComponent, component1 );
        }
    }

    protected void CreateBundle( EgoComponent egoComponent, C1 component1 )
    {
        var bundle = new EgoBundle<C1>( egoComponent, component1 );
        _bundles[ egoComponent ] = bundle;
    }

    protected void RemoveBundle( EgoComponent egoComponent )
    {
        _bundles.Remove( egoComponent );
    }

    protected void ForEachGameObject( ForEachGameObjectDelegate callback )
    {
        foreach( var bundle in _bundles.Values )
        {
            callback( bundle.egoComponent, bundle.component1 );
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
        RemoveBundle( e.egoComponent );
    }

    void Handle( AddedComponent<C1> e )
    {
        CreateBundle( e.egoComponent, e.component );
    }

    void Handle( DestroyedComponent<C1> e )
    {
        RemoveBundle( e.egoComponent );
    }
}
