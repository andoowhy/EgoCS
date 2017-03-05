using UnityEngine;

public class MouseDrag : EgoEvent
{
	public readonly EgoComponent egoComponent;

	public MouseDrag( EgoComponent egoComponent )
	{
		this.egoComponent = egoComponent;
	}
}
