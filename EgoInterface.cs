using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public abstract class EgoInterface : MonoBehaviour
{
    public abstract void EgoStart();
    public abstract void EgoUpdate();
    public abstract void EgoFixedUpdate();

    public EgoFixedUpdateSystem[] baseFixedUpdateSystems { get; protected set; }
    public EgoUpdateSystem[] baseUpdateSystems { get; protected set; }

    protected abstract EgoFixedUpdateSystem[] CreateFixedUpdateSystems();
    protected abstract EgoUpdateSystem[] CreateUpdateSystems();
}

public abstract class EgoInterface< T > : EgoInterface
    where T : EgoInterface< T >
{
    public List< EgoFixedUpdateSystem< T > > fixedUpdateSystems { get; private set; }
    public List< EgoUpdateSystem< T > > updateSystems { get; private set; }

    private T fullEgoInterface;

    public override void EgoStart()
    {
        InitSystems();

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
            foreach( var go in rootGameObjects )
            {
                var egoComponent = go.GetComponent< EgoComponent >();

                foreach( var updateSystem in updateSystems )
                {
                    updateSystem.CreateBundles( egoComponent );
                }

                foreach( var fixedUpdateSystem in fixedUpdateSystems )
                {
                    fixedUpdateSystem.CreateBundles( egoComponent );
                }
            }
        }

        EgoEvents.Start();

        // Invoke all queued Events
        EgoEvents.Invoke();

        // Clean up Destroyed Components & GameObjects
        EgoCleanUp.CleanUp();
    }

    public override void EgoUpdate()
    {
        // Update all Systems
        foreach( var updateSystem in updateSystems )
        {
#if UNITY_EDITOR
            if( updateSystem.enabled )
#endif
            {
                updateSystem.Update( fullEgoInterface );
            }
        }

        // Invoke all queued Events
        EgoEvents.Invoke();

        // Clean up Destroyed Components & GameObjects
        EgoCleanUp.CleanUp();
    }

    public override void EgoFixedUpdate()
    {
        // Update all Systems
        foreach( var fixedUpdateSystem in fixedUpdateSystems )
        {
#if UNITY_EDITOR
            if( fixedUpdateSystem.enabled )
#endif
            {
                fixedUpdateSystem.FixedUpdate( fullEgoInterface );
            }
        }

        // Clean up Destroyed Components & GameObjects
        EgoCleanUp.CleanUp();
    }

    public void InitSystems()
    {
        fullEgoInterface = this as T;

        baseFixedUpdateSystems = CreateFixedUpdateSystems();
        fixedUpdateSystems = new List<EgoFixedUpdateSystem<T>>();
        foreach( var baseFixedUpdateSystem in baseFixedUpdateSystems )
        {
            fixedUpdateSystems.Add( baseFixedUpdateSystem as EgoFixedUpdateSystem<T> );
        }

        baseUpdateSystems = CreateUpdateSystems();
        updateSystems = new List<EgoUpdateSystem<T>>();
        foreach( var baseUpdateSystem in baseUpdateSystems )
        {
            updateSystems.Add( baseUpdateSystem as EgoUpdateSystem<T> );
        }
    }

    private void InitEgoComponent( GameObject gameObject )
    {
        var egoComponent = gameObject.GetComponent< EgoComponent >();
        if( egoComponent == null ) { egoComponent = gameObject.AddComponent< EgoComponent >(); }

        egoComponent.CreateMask();

        var transform = gameObject.transform;
        var childCount = transform.childCount;
        for( var i = 0; i < childCount; i++ )
        {
            InitEgoComponent( transform.GetChild( i ).gameObject );
        }
    }
}