using UnityEngine;
using System.Collections;

public class GameCommands : MonoBehaviour
{

    static GameObject manager;

    public static void SetGameManager(GameObject newManager)
    {
        manager = newManager;
    }

    public static void ResetDepth()
    {
        manager.GetComponent<DepthManager>().GatherObjects();
    }

    public static void AddObjectToDepth(GameObject newObject)
    {

    }

    public static void GameOver()
    {
        HudManager.GameOver();
        manager.GetComponent<GameOverTimer>().Endgame(200);
    }

}
