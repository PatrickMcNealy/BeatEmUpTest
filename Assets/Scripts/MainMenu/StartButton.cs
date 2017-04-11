using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {
    
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("map1");
        }
	}

    public void onClick()
    {
        SceneManager.LoadScene("map1");
    }
}
