using UnityEngine;
using System.Collections;

public class EgoInterface : MonoBehaviour
{
	void Start()
    {
        // Add Systems here:
        //EgoSystems.Add( new ExampleSystem() );

        EgoSystems.Start();
	}
	
	void Update()
    {
        EgoSystems.Update();
    }

    void FixedUpdate()
    {
        EgoSystems.FixedUpdate();
    }
}
