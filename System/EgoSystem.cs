using UnityEngine;
using System.Collections.Generic;
using System;

public interface IEgoSystem
{
#if UNITY_EDITOR
    bool enabled { get; set; }
#endif

    void Start();
    void Update();
    void FixedUpdate();
    void CreateBundles( EgoComponent[] egoComponents );
}

public class EgoSystem : IEgoSystem
{
#if UNITY_EDITOR
    bool _enabled = true;
    public bool enabled { get { return _enabled; } set { _enabled = value; } }
#endif

    protected BitMask _mask;

    public Dictionary<EgoComponent, EgoBundle>.ValueCollection bundles { get { return null; } }

    public EgoSystem() { }

    public virtual void Start() { }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }

    public void CreateBundles( EgoComponent[] egoComponents ) { }
}
