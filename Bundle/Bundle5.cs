namespace EgoCS
{
    using UnityEngine;

    public class Bundle< TComponent1, TComponent2, TComponent3, TComponent4, TComponent5 > : Bundle
        where TComponent1 : Component
        where TComponent2 : Component
        where TComponent3 : Component
        where TComponent4 : Component
        where TComponent5 : Component
    {
        public readonly TComponent1 component1;
        public readonly TComponent2 component2;
        public readonly TComponent3 component3;
        public readonly TComponent4 component4;
        public readonly TComponent5 component5;

        public Bundle( TComponent1 component1, TComponent2 component2, TComponent3 component3, TComponent4 component4, TComponent5 component5 )
        {
            this.component1 = component1;
            this.component2 = component2;
            this.component3 = component3;
            this.component4 = component4;
            this.component5 = component5;
        }
    }
}