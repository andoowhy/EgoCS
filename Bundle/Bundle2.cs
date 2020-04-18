namespace EgoCS
{
    using UnityEngine;

    public class Bundle< TComponent1, TComponent2 > : Bundle
        where TComponent1 : Component
        where TComponent2 : Component
    {
        public readonly TComponent1 component1;
        public readonly TComponent2 component2;

        public Bundle( TComponent1 component1, TComponent2 component2 )
        {
            this.component1 = component1;
            this.component2 = component2;
        }
    }
}