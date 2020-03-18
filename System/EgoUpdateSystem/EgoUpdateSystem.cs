using System;

public abstract class EgoUpdateSystem< TEgoInterface > : EgoSystem< TEgoInterface >
    where TEgoInterface : EgoInterface
{
    public abstract void Update( TEgoInterface egoInterface );
}