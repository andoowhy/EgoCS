using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EgoSystem
{
#if UNITY_EDITOR
    public bool enabled = true;
#endif

    public abstract void CreateBundles( EgoComponent egoComponents );
}
