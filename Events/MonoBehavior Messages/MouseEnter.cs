using UnityEngine;

public class MouseEnterEvent : EgoEvent
{
	public readonly EgoComponent egoComponent;

	public MouseEnterEvent( EgoComponent egoComponent )
	{
		this.egoComponent = egoComponent;
	}
}
