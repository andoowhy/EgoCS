using UnityEngine;

public class MouseExit : EgoEvent
{
	public readonly EgoComponent egoComponent;

	public MouseExit( EgoComponent egoComponent )
	{
		this.egoComponent = egoComponent;
	}
}
