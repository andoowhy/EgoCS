using UnityEngine;

public class EgoBundle<C1, C2, C3, C4, C5, C6, C7, C8, C9, C10> : EgoBundle
    where C1 : Component
    where C2 : Component
    where C3 : Component
    where C4 : Component
    where C5 : Component
    where C6 : Component
    where C7 : Component
    where C8 : Component
    where C9 : Component
    where C10 : Component
{
    public readonly C1 component1;
    public readonly C2 component2;
    public readonly C3 component3;
    public readonly C4 component4;
    public readonly C5 component5;
    public readonly C6 component6;
    public readonly C7 component7;
    public readonly C8 component8;
    public readonly C9 component9;
    public readonly C10 component10;

    public EgoBundle( C1 component1, C2 component2, C3 component3, C4 component4, C5 component5, C6 component6, C7 component7, C8 component8, C9 component9, C10 component10 )
    {
        this.component1 = component1;
        this.component2 = component2;
        this.component3 = component3;
        this.component4 = component4;
        this.component5 = component5;
        this.component6 = component6;
        this.component7 = component7;
        this.component8 = component8;
        this.component9 = component9;
        this.component10 = component10;
    }
}
