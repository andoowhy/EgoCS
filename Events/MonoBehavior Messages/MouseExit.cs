using UnityEngine;

public class MouseExitEvent : IEgoEvent
{
	public readonly EgoComponent egoComponent;

	public MouseExitEvent( EgoComponent egoComponent )
	{
		this.egoComponent = egoComponent;
	}
}
