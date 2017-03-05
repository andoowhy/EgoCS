using UnityEngine;

public class MouseUpAsButtonEvent : EgoEvent
{
	public readonly EgoComponent egoComponent;

	public MouseUpAsButtonEvent( EgoComponent egoComponent )
	{
		this.egoComponent = egoComponent;
	}
}
