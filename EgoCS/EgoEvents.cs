using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class EgoEvent { }

public static class EgoEvents
{
    static List<Action> _invokes = new List<Action>();

    public static void AddInvoke( Action invoke )
    {
        _invokes.Add( invoke );
    }

    // Invoke all Event Queues
    public static void Invoke()
    {
        foreach( var invoke in _invokes )
        {
            invoke();
        }
    }
}

public static class EgoEvents<E> where E : EgoEvent
{
    static List<Action<E>> _handlers = new List<Action<E>>();
    static List<E> _events = new List<E>();

    static EgoEvents()
    {
        EgoEvents.AddInvoke( Invoke );
    }

    public static void Add( Action<E> handler )
    {
        _handlers.Add( handler );
    }

    public static void Queue( E e )
    {
        _events.Add( e );
    }

    public static void Invoke()
    {
        foreach( var e in _events )
        {
            foreach( var handler in _handlers )
            {
                handler.Invoke( e );
            }
        }
        _events.Clear();
    }
}
