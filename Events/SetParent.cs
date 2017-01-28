using UnityEngine;

public class SetParent : EgoEvent
{
	public readonly EgoComponent parent;
	public readonly EgoComponent child;

	public SetParent( EgoComponent parent, EgoComponent child )
	{
		this.parent = parent;
		this.child = child;
	}
}
