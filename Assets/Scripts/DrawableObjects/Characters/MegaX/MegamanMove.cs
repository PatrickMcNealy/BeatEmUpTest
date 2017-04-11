using UnityEngine;
using System.Collections;

public class MegamanMove : MonoBehaviour
{

    Vector2 spawnPoint = new Vector2(-3f, -1f);

    //public int lives = 2;

    bool hasDouble = true;

    CharacterStats charStats;
    public GameObject megaman;
    private Animator animator;
    private Gravity grav;
    private Rigidbody2D rb2d;
    public GameObject hitbox;

    bool atkPress = false;
    bool jumpPress = false;
    bool superPress = false;

    public int invFrames = 0;

    public HudManager hud;

    // Use this for initialization
    void Start()
    {
        charStats = GetComponent<CharacterStats>();
        grav = megaman.GetComponent<Gravity>();
        animator = megaman.GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (invFrames > 0)
        {
            invFrames--;
        }

        #region MOVEMENT
        Vector3 newVel = new Vector3(0f, 0f, 0f);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Knockback"))
        {
            if (megaman.GetComponent<SpriteRenderer>().flipX == true)
            {
                newVel = new Vector3(2f, 0f, 0f);
            }
            else
            {
                newVel = new Vector3(-2f, 0f, 0f);
            }
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("IdleAnimation") || animator.GetCurrentAnimatorStateInfo(0).IsName("RunAnimation") || (!grav.grounded && !animator.GetCurrentAnimatorStateInfo(0).IsName("Knockback")))
        {
            if (Input.GetKey(KeyCode.D))
            {
                animator.SetBool("HorizMove", true);
                megaman.GetComponent<SpriteRenderer>().flipX = false;
                if (Input.GetKey(KeyCode.W))
                {
                    newVel = new Vector3(6f, 2f, 0f);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    newVel = new Vector3(6f, -2f, 0f);
                }
                else
                {
                    newVel = new Vector3(6f, 0f, 0f);
                }
            }
            else if (Input.GetKey(KeyCode.A))
            {
                megaman.GetComponent<Animator>().SetBool("HorizMove", true);
                megaman.GetComponent<SpriteRenderer>().flipX = true;
                if (Input.GetKey(KeyCode.W))
                {
                    newVel = new Vector3(-6f, 2f, 0f);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    newVel = new Vector3(-6f, -2f, 0f);
                }
                else
                {
                    newVel = new Vector3(-6f, 0f, 0f);
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                megaman.GetComponent<Animator>().SetBool("HorizMove", true);
                newVel = new Vector3(0f, -2f, 0f);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                megaman.GetComponent<Animator>().SetBool("HorizMove", true);
                newVel = new Vector3(0f, 2f, 0f);
            }
            else
            {
                megaman.GetComponent<Animator>().SetBool("HorizMove", false);
                newVel = new Vector3(0f, 0f, 0f);
            }

            if (Input.GetKey(KeyCode.K))
            {
                if (!jumpPress)
                {
                    jumpPress = true;
                    if (hasDouble)
                    {
                        if (!grav.grounded)
                        {
                            hasDouble = false;
                            animator.SetTrigger("DoubleJump");
                        }
                        grav.yVel = 0.5f;
                        grav.grounded = false;
                        animator.SetBool("Grounded", grav.grounded);
                    }
                }
            }
            else
            {
                jumpPress = false;
            }

            if (grav.grounded)
            {
                hasDouble = true;
            }
        }

        if (Input.GetKey(KeyCode.J))
        {
            if (!atkPress)
            {
                atkPress = true;
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("IdleAnimation") || animator.GetCurrentAnimatorStateInfo(0).IsName("RunAnimation") || animator.GetCurrentAnimatorStateInfo(0).IsName("JumpAnimation") || animator.GetCurrentAnimatorStateInfo(0).IsName("Punch1") || animator.GetCurrentAnimatorStateInfo(0).IsName("Punch2"))
                {
                    animator.SetTrigger("Slash");
                }
            }
        }
        else
        {
            atkPress = false;
        }

        if (Input.GetKey(KeyCode.L))
        {
            if (!superPress)
            {
                superPress = true;
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("IdleAnimation") || animator.GetCurrentAnimatorStateInfo(0).IsName("RunAnimation"))
                {
                    animator.SetTrigger("Shot");
                }
            }
        }
        else
        {
            superPress = false;
        }
        rb2d.velocity = newVel;
        #endregion


        if (megaman.GetComponent<SpriteRenderer>().flipX)
        {
            hitbox.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            hitbox.transform.localScale = new Vector3(1, 1, 1);
        }


        if (megaman.transform.localPosition.y < -20f)
        {
            respawn();
        }
        else if (charStats.health <= 0)
        {
            respawn();
        }

        hud.setHealth(charStats.health, 1);
    }

    void respawn()
    {
        if (charStats.lives > 0)
        {
            charStats.health = charStats.maxHealth;
            charStats.lives--;
            grav.grounded = false;
            grav.yOffset = 10f;
            grav.transform.localPosition = new Vector3(0, grav.yOffset);
            this.transform.position = spawnPoint;
            hud.setLives(charStats.lives, 1);
            animator.Play("IdleAnimation", -1);
        }
        else
        {
            gameObject.SetActive(false);
            GameCommands.GameOver();
        }
    }


}