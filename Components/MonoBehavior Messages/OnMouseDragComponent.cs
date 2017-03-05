using UnityEngine;

[DisallowMultipleComponent]
public class OnMouseDragComponent : MonoBehaviour
{
	EgoComponent egoComponent;

	void Awake()
	{
		egoComponent = GetComponent<EgoComponent>();
	}

	void OnMouseDrag()
	{
		var onMouseDownEvent = new MouseDrag( egoComponent );
		EgoEvents<MouseDrag>.AddEvent( onMouseDownEvent );
	}
}
