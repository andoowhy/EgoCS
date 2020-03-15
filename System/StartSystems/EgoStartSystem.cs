using System;

public abstract class EgoStartSystem : EgoSystem { }

public abstract class EgoStartSystem< EI > : EgoStartSystem
    where EI : EgoInterface
{
    public abstract void Start( EI egoInterface );
}