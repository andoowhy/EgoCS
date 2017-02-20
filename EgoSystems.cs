using UnityEngine;
using UnityEngine.SceneManagement;
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
        var sceneCount = SceneManager.sceneCount;
        for( var sceneIndex = 0; sceneIndex < sceneCount; sceneIndex++ )
        {
            var scene = SceneManager.GetSceneAt( sceneIndex );
            var rootGameObjects = scene.GetRootGameObjects();

            // Attach an EgoComponent Component to every GameObject in the scene
            foreach( var go in rootGameObjects )
            {
                InitEgoComponent( go );
            }

            // Add every GameObject to any relevant system
            foreach( var system in _systems )
            {
                foreach( var go in rootGameObjects )
                {
                    var egoComponent = go.GetComponent<EgoComponent>();
                    system.CreateBundles( egoComponent );
                }
            }
        }

        EgoEvents.Start();

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

    /// <summary>
    /// Attaches and Initializes an EgoComponent on the given transform
    /// and all of its children (recursively)
    /// </summary>
    /// <param name="transform"></param>
    static void InitEgoComponent( GameObject gameObject )
    {
        var egoComponent = gameObject.GetComponent<EgoComponent>();
        if( egoComponent == null ) { egoComponent = gameObject.AddComponent<EgoComponent>(); }
        egoComponent.CreateMask();

        var transform = gameObject.transform;
        var childCount = transform.childCount;
        for( var i = 0; i < childCount; i++ )
        {
            InitEgoComponent( transform.GetChild( i ).gameObject );
        }       
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
