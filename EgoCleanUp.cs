using UnityEngine;
using System;
using System.Collections.Generic;

public class EgoCleanUp
{
    static List<Action> _cleanUps = new List<Action>();
    static List<GameObject> _destroyedGameObjects = new List<GameObject>();

    public static void AddCleanUp( Action cleanup )
    {
        _cleanUps.Add(cleanup);
    }    

    public static void CleanUp()
    {
        // Destroy Components
        foreach( var cleanUp in _cleanUps )
        {
            cleanUp();
        }

        // Destroy GameObjects
        foreach (var gameObject in _destroyedGameObjects)
        {
            UnityEngine.Object.Destroy(gameObject);
        }
        _destroyedGameObjects.Clear();
    }

    public static void Destroy( GameObject gameObject )
    {
        _destroyedGameObjects.Add( gameObject);
    }
}

public class EgoCleanUp<C>
    where C : Component
{
    static List<Tuple<EgoComponent, C>> _tuples = new List<Tuple<EgoComponent, C>>();

    static EgoCleanUp()
    {
        EgoCleanUp.AddCleanUp( CleanUp );
    }

    public static void Destroy( EgoComponent egoComponent, C component )
    {
        _tuples.Add( new Tuple<EgoComponent, C>( egoComponent, component ) );
    }

    static void CleanUp()
    {
        foreach (var tuple in _tuples)
        {
            var egoComponent = tuple.first;
            var component = tuple.second;

            egoComponent.mask[ ComponentIDs.Get( typeof( C ) ) ] = false;
            UnityEngine.Object.Destroy( component );
        }
        _tuples.Clear();
    }    
}
