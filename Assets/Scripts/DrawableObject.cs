using UnityEngine;
using System.Collections;

public class DrawableObject : MonoBehaviour {

    public bool isCharacter = false;
    float height = 0f; //Surface height for surfaces, air height for characters.
    float widthThreshold = 0f;

    public GameObject parent;
    public Transform groundTransform;
    public GameObject groundTrigger;

    public float thickness;

    public bool ifLeftPushFront;
    public bool ifRightPushFront;

    Collider2D depthCheckBox;

    public float slope = 0f;
    public bool isShot = false;
    // Use this for initialization
    void Start() {

        depthCheckBox = transform.FindChild("DepthCheck").GetComponent<Collider2D>();
        

        if (isCharacter)
        {
            groundTransform = parent.GetComponent<Transform>();
        }
        else
        {
            groundTransform = this.GetComponent<Transform>();
            height = GetComponent<SurfaceHeight>().height;
            thickness = GetComponent<Collider2D>().bounds.size.y - .1f;
        }
    }
	
	// Update is called once per frame
	void Update () {
	    
	}


    public void updateValues()
    {
        if (isCharacter)
        {
            height = this.transform.localPosition.y;
        }
    }

    //1 == see THIS object.  -1 == see dObj.
    public int CheckAgainst(DrawableObject dObj)
    {
        if(this == dObj)
        {
            return 0;
        }

        if (!depthCheckBox.IsTouching(dObj.depthCheckBox))
        {
            return 0;
        }
        else
        {
            int x = 0;
        }
        //if (!(groundTransform.position.x + distanceBetween > dObj.groundTransform.position.x && groundTransform.position.x - distanceBetween < dObj.groundTransform.position.x  ))
        //{
        //    return 0;
        //}

        this.updateValues();
        dObj.updateValues();
        if(this.isCharacter == dObj.isCharacter)
        {
            #region surfaces and objects
            if (!isCharacter)
            {
                //IF WITHIN BOUNDS
                if (groundTrigger != null)
                {
                    if (groundTrigger.GetComponent<Collider2D>().IsTouching(dObj.GetComponent<Collider2D>()))
                    {
                        if (height >= dObj.height)
                        {
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
                if(dObj.groundTrigger != null)
                {
                    if (GetComponent<Collider2D>().IsTouching(dObj.groundTrigger.GetComponent<Collider2D>()))
                    {
                        if (dObj.height >= height)
                        {
                            return -1;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                }
                

                //IF NOT COLLIDING
                if (groundTransform.position.y < dObj.groundTransform.position.y)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }

            }
            #endregion

            #region character vs character
            else
            {
                if (groundTransform.position.y < dObj.groundTransform.position.y)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            #endregion
        }
        else
        {
            if (this.isCharacter)
            {
                if(height >= dObj.height)
                {
                    return 1;
                }
                else
                {
                    if (groundTransform.position.y < dObj.groundTransform.position.y)
                    {
                        return 1;
                    }
                    else
                    {
                        
                        if (groundTransform.position.y < dObj.groundTransform.position.y + dObj.thickness)
                        {
                            if (isShot)
                            {
                                return 1;
                            }
                            float localX = 0f;
                            if (dObj.slope == 0f)
                            {
                                localX = dObj.groundTransform.position.x;
                            }
                            else
                            {
                                localX = dObj.groundTransform.position.x + ((groundTransform.position.y - (dObj.groundTransform.position.y + (dObj.thickness / 2f))) / dObj.slope);
                            }

                            if (groundTransform.position.x < localX)
                            {
                                if (dObj.ifLeftPushFront)
                                {
                                    return 1;
                                }
                            }
                            else
                            {
                                if (dObj.ifRightPushFront)
                                {
                                    return 1;
                                }
                            }
                        }

                        return -1;
                    }
                }
            }
            else
            {
                if (dObj.height >= height)
                {
                    return -1;
                }
                else
                {
                    if (groundTransform.position.y < dObj.groundTransform.position.y)
                    {
                        if (dObj.groundTransform.position.y < groundTransform.position.y + thickness)
                        {
                            if (dObj.isShot)
                            {
                                return -1;
                            }
                            float localX = 0f;
                            if (slope == 0f)
                            {
                                localX = groundTransform.position.x;
                            }
                            else
                            {
                                localX = groundTransform.position.x + ((dObj.groundTransform.position.y-(groundTransform.position.y + (thickness / 2f)) ) / slope);
                            }
                            if (dObj.groundTransform.position.x < localX)
                            {
                                if (ifLeftPushFront)
                                {
                                    return -1;
                                }
                            }
                            else
                            {
                                if (ifRightPushFront)
                                {
                                    return -1;
                                }
                            }
                        }

                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }
    }
}
