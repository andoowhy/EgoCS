using UnityEngine;

public static class Ego
{
    public static GameObject AddGameObject( GameObject gameObject )
    {
        var egoComponent = gameObject.GetComponent<EgoComponent>();
        if( !egoComponent ) egoComponent = gameObject.AddComponent<EgoComponent>();
        egoComponent.CreateMask();
        EgoEvents<AddedGameObject>.AddEvent( new AddedGameObject( gameObject, egoComponent ) );
        return gameObject;
    }

    public static void Destroy( GameObject gameObject )
    {
        var egoComponent = gameObject.GetComponent<EgoComponent>();
        EgoEvents<DestroyedGameObject>.AddEvent( new DestroyedGameObject( gameObject, egoComponent ) );

        // Destroy the GameObject in Unity
        //  Will be destroyed after Update()
        Object.Destroy( gameObject );
    }

    public static C AddComponent<C>( GameObject gameObject ) where C : Component
    {
        var egoComponent = gameObject.GetComponent<EgoComponent>();
        C component = null;
        if( !egoComponent.TryGetComponents<C>( out component ) )
        {
            component = gameObject.AddComponent<C>();
            var e = new AddedComponent<C>( component, gameObject.GetComponent<EgoComponent>() );
            EgoEvents<AddedComponent<C>>.AddEvent( e );
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

            // Destroy the Component in Unity
            Object.Destroy( e.component );

            return true;
        }

        return false;
    }
}
