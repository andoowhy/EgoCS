using UnityEngine;

public static class Ego
{
    public static EgoComponent AddGameObject( GameObject gameObject )
    {
        var egoComponent = gameObject.GetComponent<EgoComponent>();
        if( !egoComponent ) egoComponent = gameObject.AddComponent<EgoComponent>();
        egoComponent.CreateMask();
        EgoEvents<AddedGameObject>.AddEvent( new AddedGameObject( gameObject, egoComponent ) );
        return egoComponent;
    }

    public static void Destroy( GameObject gameObject )
    {
        var egoComponent = gameObject.GetComponent<EgoComponent>();
        EgoEvents<DestroyedGameObject>.AddEvent( new DestroyedGameObject( gameObject, egoComponent ) );
        EgoCleanUp.Destroy( gameObject );
    }

    public static C AddComponent<C>( GameObject gameObject ) where C : Component
    {
        var egoComponent = gameObject.GetComponent<EgoComponent>();
        C component = null;
        if( !egoComponent.TryGetComponents<C>( out component ) )
        {
            component = gameObject.AddComponent<C>();
            egoComponent.mask[ ComponentIDs.Get( typeof(C) ) ] = true; 
            EgoEvents<AddedComponent<C>>.AddEvent( new AddedComponent<C>( component, egoComponent ) );
        }

        return component;
    }

    public static bool Destroy<C>( GameObject gameObject ) where C : Component
    {
        var egoComponent = gameObject.GetComponent<EgoComponent>();
        C component = null;
        if( egoComponent.TryGetComponents<C>( out component ) )
        {
            var e = new DestroyedComponent<C>( component, egoComponent );
            EgoEvents<DestroyedComponent<C>>.AddEvent( e );
            EgoCleanUp<C>.Destroy( egoComponent, component );

            return true;
        }

        return false;
    }

    public static bool CompareMasks( BitMask egoMask, BitMask systemMask )
    {
        var mask = new BitMask( egoMask ).And( systemMask );
        return mask == systemMask;
    }
}
