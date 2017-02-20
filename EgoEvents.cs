using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EgoEvents
{
    static List<Type> _firstEvents = new List<Type>();
    static List<Type> _lastEvents = new List<Type>();

    static List<Type> _userOrderedFirstEvents = new List<Type>();
    static List<Type> _userOrderedLastEvents = new List<Type>();

    static HashSet<Type> _unorderedEvents = new HashSet<Type>();
    public static HashSet<Type> unorderedEvents
    {
        get { return _unorderedEvents; }
    }

    static Dictionary<Type, Action> _invokeLookup = new Dictionary<Type, Action>();
    public static Dictionary<Type, Action> invokeLookup
    {
        get { return _invokeLookup; }
    }

    public static void Start()
    {
        _firstEvents.Add( typeof( AddedGameObject ) );
        _lastEvents.Add( typeof( DestroyedGameObject ) );

        ComponentIDs.componentTypes.ForEach( componentType =>
        {
            MakeComponentEventInvoke( componentType, typeof( AddedComponent<> ), ref _firstEvents );
            MakeComponentEventInvoke( componentType, typeof( DestroyedComponent<> ), ref _lastEvents );
        } );
    }

    static void MakeComponentEventInvoke( Type eventType, Type genericComponentEventType, ref List<Type> eventList )
    {
        var componentEventType = genericComponentEventType.MakeGenericType( eventType );
        var fullEventType = typeof( EgoEvents<> ).MakeGenericType( componentEventType );
        if( fullEventType.IsAbstract ) { return; }
        fullEventType.TypeInitializer.Invoke( null );
        eventList.Add( componentEventType );
    }

    public static void Invoke()
    {
        _firstEvents.ForEach( t => _invokeLookup[t]() );
        _userOrderedFirstEvents.ForEach( t => _invokeLookup[t]() );
        var unordered = new HashSet<Type>( _unorderedEvents );
        foreach( var t in unordered ) { _invokeLookup[t](); }
        _userOrderedLastEvents.ForEach( t => _invokeLookup[t]() );
        _lastEvents.ForEach( t => _invokeLookup[t]() );
    }
}

public static class EgoEvents<E>
    where E : EgoEvent
{
    static List<E> _events = new List<E>();
    static List<Action<E>> _handlers = new List<Action<E>>();

    static EgoEvents()
    {
        var e = typeof( E );
        if( !EgoEvents.unorderedEvents.Contains( e ) )
        {
            EgoEvents.unorderedEvents.Add( e );
        }
        EgoEvents.invokeLookup[e] = Invoke;
    }

    static void Invoke()
    {
        var length = _events.Count;
        for( int i = 0; i < length; i++ )
        {
            foreach( var handler in _handlers )
            {
#if UNITY_EDITOR
                EgoSystem system = null;
                if( handler.Target is EgoSystem )
                {
                    system = handler.Target as EgoSystem;
                }
                else if( handler.Target is EgoConstraint )
                {
                    system = ( handler.Target as EgoConstraint ).system;
                }

                if( system != null && system.enabled )
                {
                    handler( _events[i] );
                }
#else
                handler( _events[i] );
#endif
            }
        }
        _events.RemoveRange( 0, length );
    }

    public static void AddHandler( Action<E> handler )
    {
        _handlers.Add( handler );
    }

    public static void AddEvent( E e )
    {
        _events.Add( e );
    }
}