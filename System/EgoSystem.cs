using UnityEngine;
using System.Collections.Generic;
using System;

public interface IEgoSystem
{
    void Start();
    void Update();
    void FixedUpdate();
    void CreateBundles( EgoComponent[] egoComponents );
}

public class EgoSystem : IEgoSystem
{
    protected BitMask _mask = new BitMask( ComponentIDs.GetCount() );

    public Dictionary<EgoComponent, EgoBundle>.ValueCollection bundles { get { return null; } }

    public EgoSystem() { }

    public void Start() { }

    public void Update() { }

    public void FixedUpdate() { }

    public void CreateBundles( EgoComponent[] egoComponents ) { }
}
