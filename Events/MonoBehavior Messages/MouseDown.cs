using UnityEngine;

public class MouseDown : EgoEvent
{
	public readonly EgoComponent egoComponent;

	public MouseDown( EgoComponent egoComponent )
	{
		this.egoComponent = egoComponent;
	}
}
