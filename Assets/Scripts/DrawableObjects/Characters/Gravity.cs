using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gravity : MonoBehaviour {

    public bool grounded = true;
    public GameObject parent;
    public GameObject groundCheck;
    private Collider2D parentCollider;
    private Collider2D groundTriggerCollider;


    public float yVel = 0f;
    public float yOffset = 0f;
    
    //Rigidbody2D prb;
    Animator animator;

    GameObject[] surfaces;

    // Use this for initialization
    void Start()
    {
        //prb = parent.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        surfaces = GameObject.FindGameObjectsWithTag("SurfaceSprite");
        parentCollider = parent.GetComponent<Collider2D>();
        groundTriggerCollider = groundCheck.GetComponent<Collider2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        //FALL HERE
        if (!grounded)
        {
            yVel -= 0.04f;
            yOffset += yVel;
            transform.localPosition = new Vector2(0, yOffset);
        }


        bool stayGrounded = false;

        //COMPARE SELF HEIGHT AGAINST STRUCTURE HEIGHT
        for (int i = 0; i < surfaces.Length; i++)
        {
            //IF ABOVE/ON SURFACE
            if (transform.localPosition.y >= surfaces[i].GetComponent<SurfaceHeight>().height)
            {
                Physics2D.IgnoreCollision(parentCollider, surfaces[i].GetComponentInParent<Collider2D>(), true);
                if (groundTriggerCollider.IsTouching(surfaces[i].GetComponentInParent<Collider2D>()))
                {
                    //IF COLLIDING, DO NOTHING BECAUSE YOU'RE ABOVE IT.
                    if(yOffset <= surfaces[i].GetComponent<SurfaceHeight>().height)
                    {
                        stayGrounded = true;
                    }
                }
            }
            else
            {
                if (Physics2D.GetIgnoreCollision(parentCollider, surfaces[i].GetComponentInParent<Collider2D>()))
                {

                    //IF LOWER, BUT INSIDE; LAND
                    if (groundTriggerCollider.IsTouching(surfaces[i].GetComponentInParent<Collider2D>()))
                    {
                        Physics2D.IgnoreCollision(parentCollider, surfaces[i].GetComponentInParent<Collider2D>(), true);
                        //Then LAND
                        yOffset = surfaces[i].GetComponent<SurfaceHeight>().height;
                        grounded = true;
                        GetComponent<Animator>().SetBool("Grounded", grounded);
                        transform.localPosition = new Vector2(0, yOffset);
                        stayGrounded = true;
                    }
                    //IF LOWER, BUT OUTSIDE; ENABLE COLLISION WITH SIDE.
                    else
                    {
                        Physics2D.IgnoreCollision(parentCollider, surfaces[i].GetComponentInParent<Collider2D>(), false);
                    }
                }
            }
        }

        if (!stayGrounded)
        {
            if (grounded)
            {
                grounded = false;
                animator.SetBool("Grounded", grounded);
                yVel = 0f;
            }
        }


        

    }
}
