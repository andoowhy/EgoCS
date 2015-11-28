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
        foreach( var invoke in _invokes )
        {
            invoke();
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
        foreach( var e in _events )
        {
            foreach( var handler in _handlers )
            {
                handler( e );
            }
        }
        _events.Clear();
    }
}