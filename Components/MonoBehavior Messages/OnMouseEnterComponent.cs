using UnityEngine;

[DisallowMultipleComponent]
public class OnMouseEnterComponent : MonoBehaviour
{
	EgoComponent egoComponent;

	void Awake()
	{
		egoComponent = GetComponent<EgoComponent>();
	}

	void OnMouseEnter()
	{
		var onMouseDownEvent = new MouseEnterEvent( egoComponent );
		EgoEvents<MouseEnterEvent>.AddEvent( onMouseDownEvent );
	}
}
