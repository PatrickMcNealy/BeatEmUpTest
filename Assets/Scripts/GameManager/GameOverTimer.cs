using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverTimer : MonoBehaviour
{

    int timer = -1;

    public object StateManager { get; private set; }

    // Use this for initialization
    void Start()
    {
        GameCommands.SetGameManager(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer > 0)
        {
            timer--;
        }
        else if (timer == 0)
        {
            SceneManager.LoadScene("mainmenu");
        }
    }

    public void Endgame(int timer)
    {
        this.timer = timer;
    }
}
