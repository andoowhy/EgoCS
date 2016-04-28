using UnityEngine;
using System.Collections.Generic;

public static class EgoSystems
{
    static EgoSystem[] _systems = new EgoSystem[]{};
    public static EgoSystem[] systems { get { return _systems; } }

    public static void Add( params EgoSystem[] systems )
    {
        _systems = systems;
    }

    public static void Start()
    {
        // Attach an EgoComponent Component to each GameObject
        var gameObjects = Object.FindObjectsOfType<GameObject>();
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

        // Clean up Destroyed Components & GameObjects
        EgoCleanUp.CleanUp();
    }

    public static void Update()
    {
        // Update all Systems
        foreach( var system in _systems )
        {
#if UNITY_EDITOR
            if ( system.enabled ) system.Update();
#else
            system.Update();
#endif
        }

        // Invoke all queued Events
        EgoEvents.Invoke();

        // Clean up Destroyed Components & GameObjects
        EgoCleanUp.CleanUp();
    }

    public static void FixedUpdate()
    {
        // Update all Systems
        foreach( var system in _systems )
        {
#if UNITY_EDITOR
            if( system.enabled ) system.FixedUpdate();
#else
            system.FixedUpdate();
#endif            
        }
    }
}
