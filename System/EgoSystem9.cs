using UnityEngine;
using System.Collections.Generic;

public class EgoSystem<C1, C2, C3, C4, C5, C6, C7, C8, C9> : EgoSystem
    where C1 : Component
    where C2 : Component
    where C3 : Component
    where C4 : Component
    where C5 : Component
    where C6 : Component
    where C7 : Component
    where C8 : Component
    where C9 : Component
{
    protected Dictionary<EgoComponent, EgoBundle<C1, C2, C3, C4, C5, C6, C7, C8, C9>> _bundles = new Dictionary<EgoComponent, EgoBundle<C1, C2, C3, C4, C5, C6, C7, C8, C9>>();
    
    protected delegate void ForEachGameObjectDelegate( EgoComponent egoComponent, C1 component1, C2 component2, C3 component3, C4 component4, C5 component5, C6 component6, C7 component7, C8 component8, C9 component9 );

    public EgoSystem()
    {
        _mask[ComponentIDs.Get( typeof( C1 ) )] = true;
        _mask[ComponentIDs.Get( typeof( C2 ) )] = true;
        _mask[ComponentIDs.Get( typeof( C3 ) )] = true;
        _mask[ComponentIDs.Get( typeof( C4 ) )] = true;
        _mask[ComponentIDs.Get( typeof( C5 ) )] = true;
        _mask[ComponentIDs.Get( typeof( C6 ) )] = true;
        _mask[ComponentIDs.Get( typeof( C7 ) )] = true;
        _mask[ComponentIDs.Get( typeof( C8 ) )] = true;
        _mask[ComponentIDs.Get( typeof( C9 ) )] = true;
        _mask[ComponentIDs.Get( typeof( EgoComponent ) )] = true;

        // Attach built-in Event Handlers
        EgoEvents<AddedGameObject>.AddHandler( Handle );
        EgoEvents<DestroyedGameObject>.AddHandler( Handle );
        EgoEvents<AddedComponent<C1>>.AddHandler( Handle );
        EgoEvents<AddedComponent<C2>>.AddHandler( Handle );
        EgoEvents<AddedComponent<C3>>.AddHandler( Handle );
        EgoEvents<AddedComponent<C4>>.AddHandler( Handle );
        EgoEvents<AddedComponent<C5>>.AddHandler( Handle );
        EgoEvents<AddedComponent<C6>>.AddHandler( Handle );
        EgoEvents<AddedComponent<C7>>.AddHandler( Handle );
        EgoEvents<AddedComponent<C8>>.AddHandler( Handle );
        EgoEvents<AddedComponent<C9>>.AddHandler( Handle );
        EgoEvents<DestroyedComponent<C1>>.AddHandler( Handle );
        EgoEvents<DestroyedComponent<C2>>.AddHandler( Handle );
        EgoEvents<DestroyedComponent<C3>>.AddHandler( Handle );
        EgoEvents<DestroyedComponent<C4>>.AddHandler( Handle );
        EgoEvents<DestroyedComponent<C5>>.AddHandler( Handle );
        EgoEvents<DestroyedComponent<C6>>.AddHandler( Handle );
        EgoEvents<DestroyedComponent<C7>>.AddHandler( Handle );
        EgoEvents<DestroyedComponent<C8>>.AddHandler( Handle );
        EgoEvents<DestroyedComponent<C9>>.AddHandler( Handle );
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
            var component3 = egoComponent.GetComponent<C3>();
            var component4 = egoComponent.GetComponent<C4>();
            var component5 = egoComponent.GetComponent<C5>();
            var component6 = egoComponent.GetComponent<C6>();
            var component7 = egoComponent.GetComponent<C7>();
            var component8 = egoComponent.GetComponent<C8>();
            var component9 = egoComponent.GetComponent<C9>();
            CreateBundle( egoComponent, component1, component2, component3, component4, component5, component6, component7, component8, component9  );
        }
    }

    protected void CreateBundle( EgoComponent egoComponent, C1 component1 )
    {
        if( Ego.CanUpdate( _mask, egoComponent.mask ) )
        {
            var component2 = egoComponent.GetComponent<C2>();
            var component3 = egoComponent.GetComponent<C3>();
            var component4 = egoComponent.GetComponent<C4>();
            var component5 = egoComponent.GetComponent<C5>();
            var component6 = egoComponent.GetComponent<C6>();
            var component7 = egoComponent.GetComponent<C7>();
            var component8 = egoComponent.GetComponent<C8>();
            var component9 = egoComponent.GetComponent<C9>();
            CreateBundle( egoComponent, component1, component2, component3, component4, component5, component6, component7, component8, component9  );
        }
    }

    protected void CreateBundle( EgoComponent egoComponent, C2 component2 )
    {
        if( Ego.CanUpdate( _mask, egoComponent.mask ) )
        {
            var component1 = egoComponent.GetComponent<C1>();
            var component3 = egoComponent.GetComponent<C3>();
            var component4 = egoComponent.GetComponent<C4>();
            var component5 = egoComponent.GetComponent<C5>();
            var component6 = egoComponent.GetComponent<C6>();
            var component7 = egoComponent.GetComponent<C7>();
            var component8 = egoComponent.GetComponent<C8>();
            var component9 = egoComponent.GetComponent<C9>();
            CreateBundle( egoComponent, component1, component2, component3, component4, component5, component6, component7, component8, component9  );            
        }
    }

    protected void CreateBundle( EgoComponent egoComponent, C3 component3 )
    {
        if( Ego.CanUpdate( _mask, egoComponent.mask ) )
        {
            var component1 = egoComponent.GetComponent<C1>();
            var component2 = egoComponent.GetComponent<C2>();
            var component4 = egoComponent.GetComponent<C4>();
            var component5 = egoComponent.GetComponent<C5>();
            var component6 = egoComponent.GetComponent<C6>();
            var component7 = egoComponent.GetComponent<C7>();
            var component8 = egoComponent.GetComponent<C8>();
            var component9 = egoComponent.GetComponent<C9>();
            CreateBundle( egoComponent, component1, component2, component3, component4, component5, component6, component7, component8, component9  );
        }
    }

    protected void CreateBundle( EgoComponent egoComponent, C4 component4 )
    {
        if( Ego.CanUpdate( _mask, egoComponent.mask ) )
        {
            var component1 = egoComponent.GetComponent<C1>();
            var component2 = egoComponent.GetComponent<C2>();
            var component3 = egoComponent.GetComponent<C3>();
            var component5 = egoComponent.GetComponent<C5>();
            var component6 = egoComponent.GetComponent<C6>();
            var component7 = egoComponent.GetComponent<C7>();
            var component8 = egoComponent.GetComponent<C8>();
            var component9 = egoComponent.GetComponent<C9>();
            CreateBundle( egoComponent, component1, component2, component3, component4, component5, component6, component7, component8, component9  );
        }
    }

    protected void CreateBundle( EgoComponent egoComponent, C5 component5 )
    {
        if( Ego.CanUpdate( _mask, egoComponent.mask ) )
        {
            var component1 = egoComponent.GetComponent<C1>();
            var component2 = egoComponent.GetComponent<C2>();
            var component3 = egoComponent.GetComponent<C3>();
            var component4 = egoComponent.GetComponent<C4>();
            var component6 = egoComponent.GetComponent<C6>();
            var component7 = egoComponent.GetComponent<C7>();
            var component8 = egoComponent.GetComponent<C8>();
            var component9 = egoComponent.GetComponent<C9>();
            CreateBundle( egoComponent, component1, component2, component3, component4, component5, component6, component7, component8, component9  );
        }
    }

    protected void CreateBundle( EgoComponent egoComponent, C6 component6 )
    {
        if( Ego.CanUpdate( _mask, egoComponent.mask ) )
        {
            var component1 = egoComponent.GetComponent<C1>();
            var component2 = egoComponent.GetComponent<C2>();
            var component3 = egoComponent.GetComponent<C3>();
            var component4 = egoComponent.GetComponent<C4>();
            var component5 = egoComponent.GetComponent<C5>();
            var component7 = egoComponent.GetComponent<C7>();
            var component8 = egoComponent.GetComponent<C8>();
            var component9 = egoComponent.GetComponent<C9>();
            CreateBundle( egoComponent, component1, component2, component3, component4, component5, component6, component7, component8, component9  );
        }
    }

    protected void CreateBundle( EgoComponent egoComponent, C7 component7 )
    {
        if( Ego.CanUpdate( _mask, egoComponent.mask ) )
        {
            var component1 = egoComponent.GetComponent<C1>();
            var component2 = egoComponent.GetComponent<C2>();
            var component3 = egoComponent.GetComponent<C3>();
            var component4 = egoComponent.GetComponent<C4>();
            var component5 = egoComponent.GetComponent<C5>();
            var component6 = egoComponent.GetComponent<C6>();
            var component8 = egoComponent.GetComponent<C8>();
            var component9 = egoComponent.GetComponent<C9>();
            CreateBundle( egoComponent, component1, component2, component3, component4, component5, component6, component7, component8, component9  );
        }
    }

    protected void CreateBundle( EgoComponent egoComponent, C8 component8 )
    {
        if( Ego.CanUpdate( _mask, egoComponent.mask ) )
        {
            var component1 = egoComponent.GetComponent<C1>();
            var component2 = egoComponent.GetComponent<C2>();
            var component3 = egoComponent.GetComponent<C3>();
            var component4 = egoComponent.GetComponent<C4>();
            var component5 = egoComponent.GetComponent<C5>();
            var component6 = egoComponent.GetComponent<C6>();
            var component7 = egoComponent.GetComponent<C7>();
            var component9 = egoComponent.GetComponent<C9>();
            CreateBundle( egoComponent, component1, component2, component3, component4, component5, component6, component7, component8, component9  );
        }
    }

    protected void CreateBundle( EgoComponent egoComponent, C9 component9 )
    {
        if( Ego.CanUpdate( _mask, egoComponent.mask ) )
        {
            var component1 = egoComponent.GetComponent<C1>();
            var component2 = egoComponent.GetComponent<C2>();
            var component3 = egoComponent.GetComponent<C3>();
            var component4 = egoComponent.GetComponent<C4>();
            var component5 = egoComponent.GetComponent<C5>();
            var component6 = egoComponent.GetComponent<C6>();
            var component7 = egoComponent.GetComponent<C7>();
            var component8 = egoComponent.GetComponent<C8>();
            CreateBundle( egoComponent, component1, component2, component3, component4, component5, component6, component7, component8, component9 );
        }
    }

    protected void CreateBundle( EgoComponent egoComponent, C1 component1, C2 component2, C3 component3, C4 component4, C5 component5, C6 component6, C7 component7, C8 component8, C9 component9 )
    {
        var bundle = new EgoBundle<C1, C2, C3, C4, C5, C6, C7, C8, C9>( egoComponent, component1, component2, component3, component4, component5, component6, component7, component8, component9 );
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
            callback( bundle.egoComponent, bundle.component1, bundle.component2, bundle.component3, bundle.component4, bundle.component5, bundle.component6, bundle.component7, bundle.component8, bundle.component9 );
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
        CreateBundle( e.egoComponent );
    }

    void Handle( AddedComponent<C2> e )
    {
        CreateBundle( e.egoComponent );
    }

    void Handle( AddedComponent<C3> e )
    {
        CreateBundle( e.egoComponent );
    }

    void Handle( AddedComponent<C4> e )
    {
        CreateBundle( e.egoComponent );
    }

    void Handle( AddedComponent<C5> e )
    {
        CreateBundle( e.egoComponent );
    }

    void Handle(AddedComponent<C6> e)
    {
        CreateBundle( e.egoComponent );
    }

    void Handle(AddedComponent<C7> e)
    {
        CreateBundle( e.egoComponent );
    }

    void Handle( AddedComponent<C8> e )
    {
        CreateBundle( e.egoComponent );
    }

    void Handle( AddedComponent<C9> e )
    {
        CreateBundle( e.egoComponent );
    }

    void Handle( DestroyedComponent<C1> e )
    {
        RemoveBundle( e.egoComponent );
    }

    void Handle( DestroyedComponent<C2> e )
    {
        RemoveBundle( e.egoComponent );
    }

    void Handle( DestroyedComponent<C3> e )
    {
        RemoveBundle( e.egoComponent );
    }

    void Handle( DestroyedComponent<C4> e )
    {
        RemoveBundle( e.egoComponent );
    }

    void Handle( DestroyedComponent<C5> e )
    {
        RemoveBundle( e.egoComponent );
    }

    void Handle( DestroyedComponent<C6> e )
    {
        RemoveBundle( e.egoComponent );
    }

    void Handle( DestroyedComponent<C7> e )
    {
        RemoveBundle( e.egoComponent );
    }

    void Handle( DestroyedComponent<C8> e )
    {
        RemoveBundle( e.egoComponent );
    }

    void Handle( DestroyedComponent<C9> e )
    {
        RemoveBundle( e.egoComponent );
    }
}
