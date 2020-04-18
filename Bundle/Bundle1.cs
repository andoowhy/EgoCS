namespace EgoCS
{
    using UnityEngine;

    public class Bundle< TComponent1 > : Bundle
        where TComponent1 : Component
    {
        public readonly TComponent1 component1;

        public Bundle( TComponent1 component1 )
        {
            this.component1 = component1;
        }
    }
}