using UnityEngine;

[DisallowMultipleComponent]
public class OnMouseUpAsButtonComponent : MonoBehaviour
{
	EgoComponent egoComponent;

	void Awake()
	{
		egoComponent = GetComponent<EgoComponent>();
	}

	void OnMouseUpAsButton()
	{
		var onMouseDownEvent = new MouseUpAsButton( egoComponent );
		EgoEvents<MouseUpAsButton>.AddEvent( onMouseDownEvent );
	}
}
