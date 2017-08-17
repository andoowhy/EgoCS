using UnityEngine;

public class MouseUpAsButtonEvent : IEgoEvent
{
	public readonly EgoComponent egoComponent;

	public MouseUpAsButtonEvent( EgoComponent egoComponent )
	{
		this.egoComponent = egoComponent;
	}
}
