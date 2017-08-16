using UnityEngine;

public class MouseUpEvent : IEgoEvent
{
	public readonly EgoComponent egoComponent;

	public MouseUpEvent( EgoComponent egoComponent )
	{
		this.egoComponent = egoComponent;
	}
}
