namespace EgoCS
{
    using UnityEngine;

    public class Bundle< C1 > : Bundle
        where C1 : Component
    {
        public readonly C1 component1;

        public Bundle( C1 component1 )
        {
            this.component1 = component1;
        }
    }
}