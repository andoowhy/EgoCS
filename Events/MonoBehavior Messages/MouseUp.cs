using UnityEngine;

public class MouseUpEvent : EgoEvent
{
	public readonly EgoComponent egoComponent;

	public MouseUpEvent( EgoComponent egoComponent )
	{
		this.egoComponent = egoComponent;
	}
}
