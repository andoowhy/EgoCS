using System;

public abstract class EgoSystem
{
#if UNITY_EDITOR
    public bool enabled = true;
#endif

    public EgoSystem() { }

	public virtual void CreateBundles( EgoComponent egoComponents ) { }

    public virtual void Start() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
}
