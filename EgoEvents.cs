using System;
using System.Collections.Generic;

public static class EgoEvents
{
    static List<Action> _invokes = new List<Action>();

    public static void AddInvoke( Action invoke )
    {
        _invokes.Add( invoke );
    }

    public static void Invoke()
    {
        var length = _invokes.Count;
        for( int i = 0; i < length; i++ )
        {
            _invokes[i]();
        }
    }
}

public static class EgoEvents<E>
    where E : EgoEvent
{
    static List<E> _events = new List<E>();
    static List< Action<E>  > _handlers = new List< Action<E> >();

    static EgoEvents()
    {
        EgoEvents.AddInvoke( Invoke );
    }

    public static void AddHandler( Action<E> handler )
    {
        _handlers.Add( handler );
    }

    public static void AddEvent( E e )
    {
        _events.Add( e );
    }

    public static void Invoke()
    {
        var length = _events.Count;
        for( int i = 0; i < length; i++ )
        {
            foreach( var handler in _handlers )
            {
#if UNITY_EDITOR
                var system = handler.Target as EgoSystem;
                if( system.enabled ) handler( _events[i] );
#else
                handler( _events[i] );
#endif
            }
        }
        _events.RemoveRange( 0, length );
    }
}