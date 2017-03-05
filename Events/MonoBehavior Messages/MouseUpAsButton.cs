using UnityEngine;

public class MouseUpAsButton : EgoEvent
{
	public readonly EgoComponent egoComponent;

	public MouseUpAsButton( EgoComponent egoComponent )
	{
		this.egoComponent = egoComponent;
	}
}
