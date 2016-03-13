using UnityEngine;
using System.Collections.Generic;

public class EgoInterface : MonoBehaviour
{
    static EgoInterface()
    {
        EgoSystems.systems = new List<IEgoSystem>
        {
            // Add Systems here
            //new ExampleSystem()
        };
    }

	void Start()
    {
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
