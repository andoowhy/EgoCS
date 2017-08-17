using UnityEngine;

public class MouseDragEvent : IEgoEvent
{
	public readonly EgoComponent egoComponent;

	public MouseDragEvent( EgoComponent egoComponent )
	{
		this.egoComponent = egoComponent;
	}
}
