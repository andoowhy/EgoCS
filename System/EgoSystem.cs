using UnityEngine;
using System.Collections.Generic;
using System;

//public interface IEgoSystem
//{
//#if UNITY_EDITOR
//    bool enabled { get; set; }
//#endif

//    void CreateBundles(EgoComponent[] egoComponents);

//    void Start();
//    void Update();
//    void FixedUpdate();
//}

public class EgoSystem
{
#if UNITY_EDITOR
    public bool enabled = true;
#endif

    protected BitMask _mask = new BitMask( ComponentIDs.GetCount() );

    public EgoSystem() { }

    public virtual void CreateBundles(EgoComponent[] egoComponents) { }

    public virtual void Start() { }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }
}
