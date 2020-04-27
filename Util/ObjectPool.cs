using System.Collections.Generic;
using UnityEngine;

namespace EgoCS
{
    public abstract class ObjectPool< T >
    {
        protected Stack< T > stack;

        public int count => stack.Count;

        public void Init( int stackSize )
        {
            stack = new Stack< T >(stackSize);
            for( var i = 0; i < stackSize; i++ )
            {
                stack.Push( OnInitElement() );
            }
        }

        public T Get()
        {
            if( stack.Count > 0 )
            {
                var element = stack.Pop();
                OnGet( element );
                return element;
            }

            OnPoolExhausted();

            if( stack.Count > 0 )
            {
                var element = stack.Pop();
                OnGet( element );
                return element;
            }

            Debug.LogError( "ObjectPool was exhausted and not replenished" );
            return stack.Pop();
        }

        public void Return( T element )
        {
            OnReturn( element );
            stack.Push( element );
        }

        protected abstract T OnInitElement();
        protected abstract void OnPoolExhausted();

        protected abstract void OnGet( T bitMask );

        protected abstract void OnReturn( T element );
    }
}