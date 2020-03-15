using System;

public abstract class EgoUpdateSystem : EgoSystem { }

public abstract class EgoUpdateSystem< EI > : EgoUpdateSystem
    where EI : EgoInterface
{
    public abstract void Update( EI egoInterface );
}