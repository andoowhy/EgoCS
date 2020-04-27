using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace EgoCS
{
    public class BitMaskPool : ObjectPool< BitMask >
    {
        protected override BitMask OnInitElement()
        {
            return new BitMask( ComponentUtils.GetCount() );
        }

        protected override void OnPoolExhausted()
        {
            stack.Push( new BitMask( ComponentUtils.GetCount() ) );
        }

        protected override void OnGet( BitMask bitMask )
        {
            bitMask.SetAll( false );
        }

        protected override void OnReturn( BitMask bitMask )
        {
            bitMask.SetAll( false );
        }
    }
}