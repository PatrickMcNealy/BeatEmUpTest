using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetSpawnScript : MonoBehaviour
{


    long frames = 0;
    int count = 1;
    public GameObject newMet;
    public GameObject PlayerX;
    int topCount = 300;

    DepthManager depthManager;

    // Use this for initialization
    void Start()
    {
        depthManager = GetComponent<DepthManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        frames++;

        if (frames % 150 == 0)//150
        {
            if (depthManager.objectCount < 25)
            {
                if (frames >= topCount)
                {
                    count++;
                    frames = 0;
                    topCount += 150;
                }
                for (int i = 0; i < count; i++)
                {
                    GameObject met = (GameObject)Instantiate(newMet);
                    met.transform.position = new Vector3((Random.value * 30f) - 15f, (Random.value * 6f) - 6f);
                    met.GetComponent<MetController>().target = PlayerX;
                }
                GameCommands.ResetDepth();
            }
        }
    }
}