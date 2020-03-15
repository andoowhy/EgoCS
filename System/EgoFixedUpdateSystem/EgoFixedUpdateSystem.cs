using System;

public abstract class EgoFixedUpdateSystem : EgoSystem { }

public abstract class EgoFixedUpdateSystem< EI > : EgoFixedUpdateSystem
    where EI : EgoInterface
{
    public abstract void FixedUpdate( EI egoInterface );
}