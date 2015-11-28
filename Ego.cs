using UnityEngine;

public static class Ego
{
    public static void AddGameObject( GameObject gameObject )
    {
        var egoComponent = gameObject.GetComponent<EgoComponent>();
        if( !egoComponent ) egoComponent = gameObject.AddComponent<EgoComponent>();
        egoComponent.CreateMask();

        EgoEvents<AddedGameObject>.AddEvent( new AddedGameObject( gameObject, egoComponent ) );
    }

    public static void DestroyGameObject( GameObject gameObject )
    {
        var egoComponent = gameObject.GetComponent<EgoComponent>();
        EgoEvents<DestroyedGameObject>.AddEvent( new DestroyedGameObject( gameObject, egoComponent ) );

        // Destroy the GameObject in Unity
        //  Will be destroyed after Update()
        Object.Destroy( gameObject );
    }

    public static C AddComponent<C>( GameObject gameObject ) where C : Component
    {
        var component = gameObject.AddComponent<C>();
        var e = new AddedComponent<C>( component, gameObject.GetComponent<EgoComponent>() );
        EgoEvents<AddedComponent<C>>.AddEvent( e );
        return component;
    }

    public static void DestroyComponent<C>( C component ) where C : Component
    {
        var e = new DestroyedComponent<C>( component, component.GetComponent<EgoComponent>() );
        EgoEvents<DestroyedComponent<C>>.AddEvent( e );

        // Destroy the Component in Unity
        Object.Destroy( e.component );
    }
}
