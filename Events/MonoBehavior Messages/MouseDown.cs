using UnityEngine;

public class MouseDownEvent : IEgoEvent
{
	public readonly EgoComponent egoComponent;

	public MouseDownEvent( EgoComponent egoComponent )
	{
		this.egoComponent = egoComponent;
	}
}
