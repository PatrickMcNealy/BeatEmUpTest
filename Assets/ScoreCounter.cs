using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour {

    public GameObject Hud;
    
    public void addScore(int newPoints)
    {
        Hud.GetComponent<HudManager>().addScore(newPoints, 1);
    }
}
