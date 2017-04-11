using UnityEngine;
using System.Collections;

public class MetController : MonoBehaviour {

    CharacterStats cs;
    public GameObject spriteObj;
    public GameObject metHat;
    Rigidbody2D rb2d;

    Animator anim;

    public GameObject target;

	// Use this for initialization
	void Start () {
        cs = GetComponent<CharacterStats>();
        anim = spriteObj.GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if(cs.health <= 0 || spriteObj.transform.localPosition.y < -20f)
        {
            GameObject hat = Instantiate(metHat);
            hat.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
            Destroy(gameObject);
            target.GetComponent<ScoreCounter>().addScore(100);
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Knockback"))
        {
            if (target.activeSelf)
            {
                anim.SetBool("HorizMove", true);
                if(target.transform.position.x > transform.position.x - 0.2f && target.transform.position.x < transform.position.x + 0.2f)
                {
                    if(target.transform.position.y > transform.position.y - 0.1f && target.transform.position.y < transform.position.y + 0.1f)
                    {
                        anim.SetBool("HorizMove", false);
                        rb2d.velocity = new Vector2(0f, 0f);
                    }
                    else if (target.transform.position.y > transform.position.y)
                    {
                        rb2d.velocity = new Vector2(0f, 1f);
                    }
                    else if (target.transform.position.y < transform.position.y)
                    {
                        rb2d.velocity = new Vector2(0f, -1f);
                    }
                }
                else if (target.transform.position.x > transform.position.x)
                {
                    this.transform.localScale = new Vector3(-1, 1, 1);
                    if (target.transform.position.y > transform.position.y - 0.1f && target.transform.position.y < transform.position.y + 0.1f)
                    {
                        rb2d.velocity = new Vector2(3f, 0f);
                    }
                    if (target.transform.position.y > this.transform.position.y)
                    {

                        rb2d.velocity = new Vector2(3f, 1f);
                    }
                    else
                    {
                        rb2d.velocity = new Vector2(3f, -1f);
                    }
                }
                else if (target.transform.position.x < this.transform.position.x)
                {
                    this.transform.localScale = new Vector3(1, 1, 1);
                    if (target.transform.position.y > transform.position.y - 0.1f && target.transform.position.y < transform.position.y + 0.1f)
                    {
                        rb2d.velocity = new Vector2(-3f, 0f);
                    }
                    if (target.transform.position.y > this.transform.position.y)
                    {
                        rb2d.velocity = new Vector2(-3f, 1f);
                    }
                    else
                    {
                        rb2d.velocity = new Vector2(-3f, -1f);
                    }
                }
                else
                {
                    anim.SetBool("HorizMove", false);
                    rb2d.velocity = new Vector2(0f, 0f);
                }
            }
            else
            {
                anim.SetBool("HorizMove", false);
                rb2d.velocity = new Vector2(0f, 0f);
            }
        }

        else
        {
            anim.SetBool("HorizMove", false);
            //GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        }
	}
}
