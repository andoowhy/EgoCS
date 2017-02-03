using UnityEngine;

public static class Ego
{
    public static EgoComponent AddGameObject( GameObject gameObject )
    {
		var egoComponent = AddGameObjectToChildren( gameObject.transform );
        EgoEvents<AddedGameObject>.AddEvent( new AddedGameObject( gameObject, egoComponent ) );
        return egoComponent;
    }

	private static EgoComponent AddGameObjectToChildren( Transform transform )
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			AddGameObjectToChildren( transform.GetChild( i ) );
		}

		var egoComponent = transform.GetComponent<EgoComponent>();
		if( egoComponent == null )
		{
			egoComponent = transform.gameObject.AddComponent<EgoComponent>();
		}
		
		egoComponent.CreateMask();
		
		return egoComponent;
	}

	public static C AddComponent<C>( EgoComponent egoComponent ) where C : Component
    {
		C component = null;
		if( !egoComponent.TryGetComponents<C>( out component ) )
		{
			component = egoComponent.gameObject.AddComponent<C>();
			egoComponent.mask[ ComponentIDs.Get( typeof( C ) ) ] = true;
			EgoEvents<AddedComponent<C>>.AddEvent( new AddedComponent<C>( component, egoComponent ) );
		}

		return component;
	}

	public static void DestroyGameObject( EgoComponent egoComponent )
	{
		var gameObject = egoComponent.gameObject;
		EgoEvents<DestroyedGameObject>.AddEvent( new DestroyedGameObject( gameObject, egoComponent ) );
		EgoCleanUp.Destroy( egoComponent.gameObject );
	}

	public static bool DestroyComponent<C>( EgoComponent egoComponent ) where C : Component
	{
		C component = null;
		if( !egoComponent.TryGetComponents<C>( out component ) ){ return false; }

		var e = new DestroyedComponent<C>( component, egoComponent );
		EgoEvents<DestroyedComponent<C>>.AddEvent( e );
		EgoCleanUp<C>.Destroy( egoComponent, component );
		return true;
	}

	public static void SetParent( EgoComponent parent, EgoComponent child )
	{
		if( child == null ){ Debug.LogWarning( "Cannot set the Parent of a null Child" ); }
		EgoEvents<SetParent>.AddEvent( new SetParent( parent, child ) );
	}
}
