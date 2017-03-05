using UnityEngine;

public class MouseEnter : EgoEvent
{
	public readonly EgoComponent egoComponent;

	public MouseEnter( EgoComponent egoComponent )
	{
		this.egoComponent = egoComponent;
	}
}
