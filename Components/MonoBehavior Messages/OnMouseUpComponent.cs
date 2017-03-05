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
		var onMouseDownEvent = new MouseUp( egoComponent );
		EgoEvents<MouseUp>.AddEvent( onMouseDownEvent );
	}
}
