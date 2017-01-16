using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class HudManager : MonoBehaviour {

    public GameObject livesText;
    public GameObject healthSlider;
    public GameObject score;
    static Text ObjectCounter;
    static Text gameOver;
    

	// Use this for initialization
	void Start () {
        ObjectCounter = transform.FindChild("ObjectCounter").GetComponent<Text>();
        gameOver = transform.FindChild("GameOver").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void setLives(int lives, int player)
    {
        livesText.GetComponent<Text>().text = "x" + lives;
    }

    public void setHealth(int health, int player)
    {
        healthSlider.GetComponent<Slider>().value = health;
    }

    public void addScore(int addScore, int player)
    {
        Text text = score.GetComponent<Text>();

        int val;
        Int32.TryParse(text.text, out val);

        val += addScore;
        text.text = val.ToString();
    }

    public static void countObjects(int objects)
    {
        ObjectCounter.text = objects.ToString();
    }

    public static void GameOver()
    {
        gameOver.gameObject.SetActive(true);
    }

}