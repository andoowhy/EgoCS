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
		for( var i = 0; i < _cleanUps.Count; i++ )
		{
			_cleanUps[ i ]();
		}

        // Destroy GameObjects
		for( var i = 0; i < _destroyedGameObjects.Count; i++ )
		{
			UnityEngine.Object.Destroy( _destroyedGameObjects[ i ] );
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
		for( var i = 0; i < _tuples.Count; i++ )
		{
			var egoComponent = _tuples[ i ].first;
			var component = _tuples[ i ].second;

			egoComponent.mask[ ComponentIDs.Get( typeof( C ) ) ] = false;
			UnityEngine.Object.Destroy( component );
		}
        _tuples.Clear();
    }    
}
