using UnityEngine;
using System.Collections.Generic;

public class EgoInterface : MonoBehaviour
{
	static EgoInterface()
	{
        //Add Systems here:
        EgoSystems.Add(
            // Game
            new BrickInstantiationSystem(),
            new BrickSystem(),
            new BallSystem(),
            new GameEndSystem(),
            new PaddleSystem(),
            new ScoreSystem(),

            //UI
            new UISystem(),
            new PlayAgainButtonSystem()
        );

        //EgoEvents.SetLastEvents<
        //   ResetGameEvent
        //>();
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
