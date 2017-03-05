using UnityEngine;

[DisallowMultipleComponent]
public class OnMouseUpComponent : MonoBehaviour
{
	EgoComponent egoComponent;

	void Awake()
	{
		egoComponent = GetComponent<EgoComponent>();
	}

	void OnMouseUp()
	{
		var onMouseDownEvent = new MouseUpEvent( egoComponent );
		EgoEvents<MouseUpEvent>.AddEvent( onMouseDownEvent );
	}
}
