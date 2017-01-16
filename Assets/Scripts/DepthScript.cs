using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DepthScript : MonoBehaviour {

    GameObject[] allGO;
    //List<GameObject> allGO;

    string[] names;

    public int objectCount;

	// Use this for initialization
	void Start () {
        GatherObjects();
    }
	
    public void GatherObjects()
    {
        GameObject[] surfaces = GameObject.FindGameObjectsWithTag("Surface");
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerSprite");

        allGO = new GameObject[surfaces.Length + players.Length];
        int j = 0;
        for (int i = 0; i < surfaces.Length; i++)
        {
            allGO[j] = surfaces[i];
            j++;
        }
        for (int i = 0; i < players.Length; i++)
        {
            allGO[j] = players[i];
            j++;
        }

        names = new string[allGO.Length];
        for(int i = 0; i< allGO.Length; i++)
        {
            names[i] = allGO[i].name;
        }
    }

    public void addObject(GameObject newObject)
    {
        GameObject[] tempArray = new GameObject[allGO.Length + 1];
        for (int tempCounter = 0; tempCounter < tempArray.Length; tempCounter++)
        {
            tempArray[tempCounter] = allGO[tempCounter];
        }
        tempArray[tempArray.Length] = newObject;
        allGO = tempArray;
    }


    // Update is called once per frame
    void FixedUpdate() {

        #region RemoveNulls
        //CHECK FOR NULLS AND REMOVE THEM FROM THE LIST.
        for (int i = 0; i < allGO.Length; i++)
        {
            if (allGO[i] == null)
            {
                GameObject[] tempArray = new GameObject[allGO.Length - 1];
                int AllGOCounter = 0;
                for (int tempCounter = 0; tempCounter < tempArray.Length; tempCounter++)
                {
                    if (AllGOCounter == i)
                    {
                        AllGOCounter++;
                    }
                    tempArray[tempCounter] = allGO[AllGOCounter];
                    AllGOCounter++;
                }
                allGO = tempArray;
            }
        }
        #endregion

        //REORDER LIST
        #region newSort
        for (int lowCompare = 0; lowCompare < allGO.Length; lowCompare++)
        {
            for(int highCompare = lowCompare + 1; highCompare < allGO.Length; highCompare++)
            {
                int compareResult = allGO[lowCompare].GetComponent<DrawableObject>().CheckAgainst(allGO[highCompare].GetComponent<DrawableObject>());

                if (compareResult > 0)
                {
                    GameObject temp = allGO[lowCompare];
                    allGO[lowCompare] = allGO[highCompare];
                    allGO[highCompare] = temp;
                }
            }
        }
        #endregion

        #region oldSort
        //for(int i = 0; i < allGO.Length - 1; i++)
        //{
        //    int compareResult = allGO[i].GetComponent<DrawableObject>().CheckAgainst(allGO[i + 1].GetComponent<DrawableObject>());

        //    if (compareResult > 0)
        //    {
        //        GameObject temp = allGO[i + 1];
        //        allGO[i + 1] = allGO[i];
        //        allGO[i] = temp;
        //    }
        //    else if (compareResult == 0)
        //    {
        //        if (i + 2 < allGO.Length)
        //        {
        //            if (allGO[i].GetComponent<DrawableObject>().CheckAgainst(allGO[i + 2].GetComponent<DrawableObject>()) > 0)
        //            {
        //                GameObject temp = allGO[i + 1];
        //                allGO[i + 1] = allGO[i];
        //                allGO[i] = temp;
        //            }
        //        }
        //        else if (i > 0)
        //        {
        //            if (allGO[i-1].GetComponent<DrawableObject>().CheckAgainst(allGO[i+1].GetComponent<DrawableObject>()) > 0)
        //            {
        //                GameObject temp = allGO[i + 1];
        //                allGO[i + 1] = allGO[i-1];
        //                allGO[i-1] = temp;
        //            }
        //        }
        //    }
        //}
        #endregion

        #region TESTING NAMES
        for (int i = 0; i < allGO.Length; i++)
        {
            names[i] = allGO[i].name;
        }
        #endregion

        //ASSIGN ITEMS DEPTH BASED ON ORDER
        for (int i = 0; i< allGO.Length; i++)
        {
            allGO[i].GetComponent<SpriteRenderer>().sortingOrder = i;
        }

        HudManager.countObjects(allGO.Length);
        objectCount = allGO.Length;
    }
}
