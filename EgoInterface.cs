using UnityEngine;
using System.Collections;

public class EgoInterface : MonoBehaviour
{
	void Start()
    {
        // Add Systems here:
        EgoSystems.Add( new BallSystem() );
        EgoSystems.Add( new BrickSystem() );
        EgoSystems.Add( new BrickInstantiationSystem() );
        EgoSystems.Add( new PaddleSystem() );

        // Win Conditions
        EgoSystems.Add( new ScoreSystem() );
        EgoSystems.Add( new GameEndSystem() );

        // UI
        EgoSystems.Add( new PlayAgainButtonSystem() );
        EgoSystems.Add( new UISystem() );

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
