using UnityEngine;

public class MouseUp : EgoEvent
{
	public readonly EgoComponent egoComponent;

	public MouseUp( EgoComponent egoComponent )
	{
		this.egoComponent = egoComponent;
	}
}
