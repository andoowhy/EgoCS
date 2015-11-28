using UnityEngine;
using System.Collections;

public class EgoInterface : MonoBehaviour
{
	void Start()
    {
        // Add Systems here:
        //EgoSystems.Add( new ExampleSystem() );

        EgoSystems.Start();
        EgoEvents.Invoke();
	}
	
	void Update()
    {
        EgoSystems.Update();
        EgoEvents.Invoke();
    }

    void FixedUpdate()
    {
        EgoSystems.FixedUpdate();
    }
}
