using UnityEngine;

public class MouseEnterEvent : IEgoEvent
{
	public readonly EgoComponent egoComponent;

	public MouseEnterEvent( EgoComponent egoComponent )
	{
		this.egoComponent = egoComponent;
	}
}
