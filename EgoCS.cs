namespace EgoCS
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public abstract class EgoCS : MonoBehaviour
    {
        public abstract void EgoStart();
        public abstract void EgoUpdate();
        public abstract void EgoFixedUpdate();

        public EgoSystem[] baseFixedUpdateSystems { get; protected set; }
        public EgoSystem[] baseUpdateSystems { get; protected set; }

        #region Constraint Callbacks

        protected List< Action< EgoComponent > > addedGameObjectCallbacks;
        protected List< Action< EgoComponent > > destroyedGameObjectCallbacks;
        protected List< Action< EgoComponent, EgoComponent, bool > > setParentCallbacks;
        protected Dictionary< Type, List< Action< EgoComponent > > > addedComponentCallbacks;
        protected Dictionary< Type, List< Action< EgoComponent > > > destroyedComponentCallbacks;

        protected List< Type > addedComponentTypes;
        protected List< Type > destroyedComponentTypes;

        protected List< EgoComponent > addedGameObjects;
        protected List< EgoComponent > destroyedGameObjects;
        protected List< (EgoComponent, EgoComponent, bool) > setParents;
        protected Dictionary< Type, List< (EgoComponent, Component) > > addedComponents;
        protected Dictionary< Type, List< (EgoComponent, Component) > > destroyedComponents;

        protected void InitConstraintCallbacks()
        {
            ComponentUtils.Init();

            addedGameObjectCallbacks = new List< Action< EgoComponent > >();
            destroyedGameObjectCallbacks = new List< Action< EgoComponent > >();
            setParentCallbacks = new List< Action< EgoComponent, EgoComponent, bool > >();

            addedComponentCallbacks = new Dictionary< Type, List< Action< EgoComponent > > >();
            destroyedComponentCallbacks = new Dictionary< Type, List< Action< EgoComponent > > >();

            addedComponentTypes = new List< Type >();
            destroyedComponentTypes = new List< Type >();

            foreach( var kvp in ComponentUtils.types )
            {
                var componentType = kvp.Key;
                addedComponentCallbacks.Add( componentType, new List< Action< EgoComponent > >() );
                destroyedComponentCallbacks.Add( componentType, new List< Action< EgoComponent > >() );
            }

            addedGameObjects = new List< EgoComponent >();
            destroyedGameObjects = new List< EgoComponent >();
            setParents = new List< (EgoComponent, EgoComponent, bool) >();

            addedComponents = new Dictionary< Type, List< (EgoComponent, Component) > >();
            destroyedComponents = new Dictionary< Type, List< (EgoComponent, Component) > >();

            foreach( var kvp in ComponentUtils.types )
            {
                var componentType = kvp.Key;
                addedComponents.Add( componentType, new List< (EgoComponent, Component) >() );
                destroyedComponents.Add( componentType, new List< (EgoComponent, Component) >() );
            }
        }

        public void AddAddedGameObjectCallback( Action< EgoComponent > callback )
        {
            addedGameObjectCallbacks.Add( callback );
        }

        public void AddDestroyedGameObjectCallback( Action< EgoComponent > callback )
        {
            destroyedGameObjectCallbacks.Add( callback );
        }

        public void AddSetParentCallback( Action< EgoComponent, EgoComponent, bool > callback )
        {
            setParentCallbacks.Add( callback );
        }

        public void AddDestroyedComponentCallback( Type componentType, Action< EgoComponent > callback )
        {
            destroyedComponentCallbacks[ componentType ].Add( callback );
        }

        public void AddAddedComponentCallback( Type componentType, Action< EgoComponent > callback )
        {
            destroyedComponentCallbacks[ componentType ].Add( callback );
        }

        #endregion
    }

    public abstract class EgoCS< T > : EgoCS
        where T : EgoCS< T >
    {
        public List< EgoFixedUpdateSystem< T > > fixedUpdateSystems { get; private set; }
        public List< UpdateSystem< T > > updateSystems { get; private set; }

        private T fullEgoInterface;

        protected abstract EgoFixedUpdateSystem< T >[] CreateFixedUpdateSystems();
        protected abstract UpdateSystem< T >[] CreateUpdateSystems();


        #region MonoBehaviour Methods

        public override void EgoStart()
        {
            InitConstraintCallbacks();

            InitSystems();

            InitEgoComponentsAndBundles();

            UpdateConstraints();
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

            UpdateConstraints();
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

            UpdateConstraints();
        }

        private void InitSystems()
        {
            fullEgoInterface = this as T;

            baseFixedUpdateSystems = CreateFixedUpdateSystems();
            fixedUpdateSystems = new List< EgoFixedUpdateSystem< T > >();
            foreach( var baseFixedUpdateSystem in baseFixedUpdateSystems )
            {
                fixedUpdateSystems.Add( baseFixedUpdateSystem as EgoFixedUpdateSystem< T > );
            }

            baseUpdateSystems = CreateUpdateSystems();
            updateSystems = new List< UpdateSystem< T > >();
            foreach( var baseUpdateSystem in baseUpdateSystems )
            {
                updateSystems.Add( baseUpdateSystem as UpdateSystem< T > );
            }
        }

        private void InitEgoComponentsAndBundles()
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

        public void UpdateConstraints()
        {
            #region Constraint Callbacks

            foreach( var addedGameObjectEgoComponent in addedGameObjects )
            {
                foreach( var callback in addedGameObjectCallbacks )
                {
                    callback( addedGameObjectEgoComponent );
                }
            }

            foreach( var kvp in addedComponents )
            {
                var callbacks = addedComponentCallbacks[ kvp.Key ];

                foreach( var addedComponent in kvp.Value )
                {
                    foreach( var callback in callbacks )
                    {
                        callback( addedComponent.Item1 );
                    }
                }
            }

            foreach( var (parent, child, worldPositionStays) in setParents )
            {
                foreach( var callback in setParentCallbacks )
                {
                    callback( parent, child, worldPositionStays );
                }
            }

            foreach( var kvp in destroyedComponents )
            {
                var callbacks = destroyedComponentCallbacks[ kvp.Key ];

                foreach( var destroyedComponent in kvp.Value )
                {
                    foreach( var callback in callbacks )
                    {
                        callback( destroyedComponent.Item1 );
                    }
                }
            }

            foreach( var destroyedGameObjectEgoComponent in destroyedGameObjects )
            {
                foreach( var callback in destroyedGameObjectCallbacks )
                {
                    callback( destroyedGameObjectEgoComponent );
                }
            }

            #endregion

            #region Destroy

            foreach( var kvp in destroyedComponents )
            {
                foreach( var (_, component) in kvp.Value )
                {
                    Destroy( component );
                }
            }

            foreach( var egoComponent in destroyedGameObjects )
            {
                Destroy( egoComponent.gameObject );
            }

            #endregion

            #region Cleanup

            addedGameObjectCallbacks.Clear();
            destroyedGameObjectCallbacks.Clear();
            setParents.Clear();
            foreach( var kvp in addedComponents )
            {
                addedComponents[ kvp.Key ].Clear();
            }

            foreach( var kvp in destroyedComponents )
            {
                destroyedComponents[ kvp.Key ].Clear();
            }

            #endregion
        }

        #endregion

        public EgoComponent AddGameObject( GameObject gameObject )
        {
            var egoComponent = AddGameObject( gameObject.transform );
            addedGameObjects.Add( egoComponent );
            return egoComponent;
        }

        private EgoComponent AddGameObject( Transform transform )
        {
            for( int i = 0; i < transform.childCount; i++ )
            {
                AddGameObject( transform.GetChild( i ) );
            }

            var egoComponent = transform.GetComponent< EgoComponent >();
            if( egoComponent == null )
            {
                egoComponent = transform.gameObject.AddComponent< EgoComponent >();
            }

            egoComponent.CreateMask();

            return egoComponent;
        }

        public C AddComponent< C >( EgoComponent egoComponent )
            where C : Component
        {
            if( egoComponent.TryGetComponents< C >( out var component ) ) { return component; }

            component = egoComponent.gameObject.AddComponent< C >();
            egoComponent.mask[ ComponentUtils.Get( typeof( C ) ) ] = true;
            addedComponents[ typeof( C ) ].Add( ( egoComponent, component ) );

            return component;
        }

        public void DestroyGameObject( EgoComponent egoComponent )
        {
            destroyedGameObjects.Add( egoComponent );
        }

        public bool DestroyComponent< C >( EgoComponent egoComponent ) where C : Component
        {
            if( !egoComponent.TryGetComponents< C >( out var component ) ) { return false; }

            destroyedComponents[ typeof( C ) ].Add( ( egoComponent, component ) );
            return true;
        }

        public void SetParent( EgoComponent parent, EgoComponent child, bool worldPositionStays )
        {
            if( child == null ) { Debug.LogError( "Cannot set the Parent of a null Child" ); }

            if( child == parent ) { Debug.LogError( "Cannot set Child to be its own Parent" ); }

            setParents.Add( ( parent, child, worldPositionStays ) );
        }
    }
}