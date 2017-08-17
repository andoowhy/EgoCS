using UnityEngine;

public class SetParent : IEgoEvent
{
	public readonly EgoComponent parent;
	public readonly EgoComponent child;
	public readonly bool worldPositionStays;

	public SetParent( EgoComponent parent, EgoComponent child, bool worldPositionStays = true )
	{
		this.parent = parent;
		this.child = child;
		this.worldPositionStays = worldPositionStays;
	}
}
