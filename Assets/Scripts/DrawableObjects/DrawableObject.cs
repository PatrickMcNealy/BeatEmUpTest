using UnityEngine;
using System.Collections;

//DrawableObject is applied to sprite objects, as it deals with handling gravity and drawing depth.

public class DrawableObject : MonoBehaviour {

    [SerializeField]
    bool isCharacter = false;
    float height = 0f; //Surface height for surfaces, air height for characters.
    //float widthThreshold = 0f;

        [SerializeField]
    GameObject parent;
    Transform groundTransform; //Point of Origin


    [SerializeField]
    GameObject groundTrigger; //Used by platforms, to see if they share the same ground space (for instance, table is on top of ground.)

    float thickness;


    //Used by platforms, use to determine how a character should be displayed, based on each side of an object.  This allows for more shapes, like trapezoids.
    [SerializeField]
    bool ifLeftPushFront;
    [SerializeField]
    bool ifRightPushFront;

    Collider2D depthCheckBox; //Checks the area in which it's neccesary to organize for depth.

    public float slope = 0f;
    public bool isShot = false;

    // Use this for initialization
    void Start()
    {
        depthCheckBox = transform.FindChild("DepthCheck").GetComponent<Collider2D>();
        

        if (isCharacter)
        {
            groundTransform = parent.GetComponent<Transform>();
        }
        else
        {
            groundTransform = this.GetComponent<Transform>();
            height = GetComponent<SurfaceHeight>().height;
            thickness = parent.GetComponent<Collider2D>().bounds.size.y - .1f;
        }
    }
	
    public void UpdateValues()
    {
        if (isCharacter)
        {
            height = this.transform.localPosition.y;
        }
    }

    //1 == see THIS object in front.  -1 == see dObj in front.
    public int CheckDepthAgainst(DrawableObject dObj)
    {
        //If object is being compared with itself, do not swap.
        if (this == dObj)
        {
            return 0;
        }

        //If not within neccesary range on screen, don't organize.
        if (!depthCheckBox.IsTouching(dObj.depthCheckBox))
        {
            return 0;
        }


        this.UpdateValues();
        dObj.UpdateValues();
        if(this.isCharacter == dObj.isCharacter)//If both objects are of the same type.
        {
            #region surfaces and objects
            if (!isCharacter)
            {
                //IF WITHIN BOUNDS
                if (groundTrigger != null)
                {
                    if (groundTrigger.GetComponent<Collider2D>().IsTouching(dObj.GetComponentInParent<Collider2D>()))
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
                    if (parent.GetComponent<Collider2D>().IsTouching(dObj.groundTrigger.GetComponent<Collider2D>()))
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
