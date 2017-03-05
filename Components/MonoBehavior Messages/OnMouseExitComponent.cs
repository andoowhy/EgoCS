using UnityEngine;

[DisallowMultipleComponent]
public class OnMouseExitComponent : MonoBehaviour
{
	EgoComponent egoComponent;

	void Awake()
	{
		egoComponent = GetComponent<EgoComponent>();
	}

	void OnMouseExit()
	{
		var onMouseDownEvent = new MouseExit( egoComponent );
		EgoEvents<MouseExit>.AddEvent( onMouseDownEvent );
	}
}
