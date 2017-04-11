using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DepthManager : MonoBehaviour {

    GameObject[] allSprites;
    //List<GameObject> allGO;

    string[] names;

    public int objectCount;

	// Use this for initialization
	void Start () {
        GatherObjects();
    }
	
    public void GatherObjects()
    {
        GameObject[] surfaces = GameObject.FindGameObjectsWithTag("SurfaceSprite");
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerSprite");

        allSprites = new GameObject[surfaces.Length + players.Length];
        int j = 0;
        for (int i = 0; i < surfaces.Length; i++)
        {
            allSprites[j] = surfaces[i];
            j++;
        }
        for (int i = 0; i < players.Length; i++)
        {
            allSprites[j] = players[i];
            j++;
        }

        names = new string[allSprites.Length];
        for(int i = 0; i< allSprites.Length; i++)
        {
            names[i] = allSprites[i].name;
        }
    }

    public void AddObject(GameObject newObject)
    {
        GameObject[] tempArray = new GameObject[allSprites.Length + 1];
        for (int tempCounter = 0; tempCounter < tempArray.Length; tempCounter++)
        {
            tempArray[tempCounter] = allSprites[tempCounter];
        }
        tempArray[tempArray.Length] = newObject;
        allSprites = tempArray;
    }


    // Update is called once per frame
    void Update() {

        #region RemoveNulls
        //CHECK FOR NULLS AND REMOVE THEM FROM THE LIST.
        for (int i = 0; i < allSprites.Length; i++)
        {
            if (allSprites[i] == null)
            {
                GameObject[] tempArray = new GameObject[allSprites.Length - 1];
                int spriteCounter = 0;
                for (int tempCounter = 0; tempCounter < tempArray.Length; tempCounter++)
                {
                    if (spriteCounter == i)
                    {
                        spriteCounter++;
                    }
                    tempArray[tempCounter] = allSprites[spriteCounter];
                    spriteCounter++;
                }
                allSprites = tempArray;
                i--;
            }
        }
        #endregion

        //REORDER LIST
        //A faster or guaranteed sorting algorithm can't be used because there is no "correct" order of sprites.  A<B<C<A can technically be correct.
        #region SortDepth
        for (int lowCompare = 0; lowCompare < allSprites.Length; lowCompare++)
        {
            for(int highCompare = lowCompare +1; highCompare < allSprites.Length; highCompare++)
            {
                int compareResult = allSprites[lowCompare].GetComponent<DrawableObject>().CheckDepthAgainst(allSprites[highCompare].GetComponent<DrawableObject>());

                if (compareResult > 0)
                {
                    GameObject temp = allSprites[lowCompare];
                    allSprites[lowCompare] = allSprites[highCompare];
                    allSprites[highCompare] = temp;
                }
            }
        }
        #endregion

        //ASSIGN ITEMS DEPTH VALUES BASED ON ORDER
        for (int i = 0; i< allSprites.Length; i++)
        {
            allSprites[i].GetComponent<SpriteRenderer>().sortingOrder = i;
        }

        HudManager.countObjects(allSprites.Length);
        objectCount = allSprites.Length;
    }
}
