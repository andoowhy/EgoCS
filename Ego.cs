using UnityEngine;

public static class Ego
{
    public static EgoComponent AddGameObject( GameObject gameObject )
    {
        var egoComponent = gameObject.GetComponent<EgoComponent>();
		if( egoComponent == null ){ egoComponent = gameObject.AddComponent<EgoComponent>(); }
        egoComponent.CreateMask();
        EgoEvents<AddedGameObject>.AddEvent( new AddedGameObject( gameObject, egoComponent ) );
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
		EgoEvents<DestroyedGameObject>.AddEvent( new DestroyedGameObject( egoComponent.gameObject, egoComponent ) );
	}

	public static bool DestroyComponent<C>( EgoComponent egoComponent ) where C : Component
	{
		C component = null;
		if( egoComponent.TryGetComponents<C>( out component ) )
		{
			var e = new DestroyedComponent<C>( component, egoComponent );
			EgoEvents<DestroyedComponent<C>>.AddEvent( e );
			EgoCleanUp<C>.Destroy( egoComponent, component );

			return true;
		}

		return false;
	}

	public static void SetParent( EgoComponent parent, EgoComponent child )
	{
		if( child == null ){ Debug.LogWarning( "Cannot set the Parent of a null Child" ); }
		EgoEvents<SetParent>.AddEvent( new SetParent( parent, child ) );
	}
}
