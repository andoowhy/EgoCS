using UnityEngine;

public class MouseDownEvent : EgoEvent
{
	public readonly EgoComponent egoComponent;

	public MouseDownEvent( EgoComponent egoComponent )
	{
		this.egoComponent = egoComponent;
	}
}
