using UnityEngine;
using System.Collections;

public class GameCommands : MonoBehaviour {

    static GameObject manager;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void SetGameManager(GameObject newManager)
    {
        manager = newManager; 
    }

    public static void ResetDepth()
    {
        manager.GetComponent<DepthScript>().GatherObjects();
    }

    public static void AddObjectToDepth(GameObject newObject)
    {

    }

    public static void GameOver()
    {
        HudManager.GameOver();
        manager.GetComponent<Startup>().Endgame(200);
    }

}
