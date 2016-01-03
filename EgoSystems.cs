using UnityEngine;
using System.Collections.Generic;

public static class EgoSystems
{
    private static List<IEgoSystem> _systems = new List<IEgoSystem>();

    public static void Add( IEgoSystem system )
    {
        _systems.Add( system );
    }

    public static void Start()
    {
        // Attach an EgoComponent Component to each GameObject
        var gameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        var egoComponents = new List<EgoComponent>();
        foreach( var gameObject in gameObjects )
        {
            var egoComponent = gameObject.GetComponent<EgoComponent>();
            if( !egoComponent ) egoComponent = gameObject.AddComponent<EgoComponent>();
            egoComponent.CreateMask();
			egoComponents.Add( egoComponent );
        }

        // Create System bundles
        foreach( var system in _systems )
        {
            system.CreateBundles( egoComponents.ToArray() );
        }

        // Start all Systems
        foreach( var system in _systems )
        {
            system.Start();
        }

        // Invoke all queued Events
        EgoEvents.Invoke();
    }

    public static void Update()
    {
        // Update all Systems
        foreach( var system in _systems )
        {
            system.Update();
        }

        // Invoke all queued Events
        EgoEvents.Invoke();
    }

    public static void FixedUpdate()
    {
        // Update all Systems
        foreach( var system in _systems )
        {
            system.FixedUpdate();
        }
    }
}
