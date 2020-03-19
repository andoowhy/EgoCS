using System;

public abstract class EgoFixedUpdateSystem< TEgoInterface > : EgoSystem< TEgoInterface >
    where TEgoInterface : EgoInterface
{
    public abstract void FixedUpdate( TEgoInterface egoInterface );
}