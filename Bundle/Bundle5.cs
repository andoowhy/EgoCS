﻿namespace EgoCS
{
    using UnityEngine;

    public class Bundle< C1, C2, C3, C4, C5 > : Bundle
        where C1 : Component
        where C2 : Component
        where C3 : Component
        where C4 : Component
        where C5 : Component
    {
        public readonly C1 component1;
        public readonly C2 component2;
        public readonly C3 component3;
        public readonly C4 component4;
        public readonly C5 component5;

        public Bundle( C1 component1, C2 component2, C3 component3, C4 component4, C5 component5 )
        {
            this.component1 = component1;
            this.component2 = component2;
            this.component3 = component3;
            this.component4 = component4;
            this.component5 = component5;
        }
    }
}