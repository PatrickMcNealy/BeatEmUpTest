using UnityEngine;
using System.Collections;

public class SpriteBase : MonoBehaviour
{
    public GameObject parent;
    Animator animator;
    Transform pTrans;
    Rigidbody2D pRB2D;

    float knockbackDirection = 0f;

    CharacterStats cs;

    bool isPlayer = false;

    // Use this for initialization
    void Start()
    {
        if (parent.GetComponent<MegamanMove>())
        {
            isPlayer = true;
        }
        cs = parent.GetComponent<CharacterStats>();
        pTrans = parent.GetComponent<Transform>();
        animator = GetComponent<Animator>();
        pRB2D = parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Knockback"))
        {
            pRB2D.velocity = new Vector2(knockbackDirection, 0f);
        }
        else
        {
            //pRB2D.velocity = new Vector2(0f, 0f);
        }
    }

    public void OnHit(bool isFaceRight, int damage, float depth)
    {
        if (cs.invFrames <= 0)
        {
            if (depth > pTrans.position.y - 0.2f && depth < pTrans.position.y + 0.2f)
            {
                Debug.Log("HIT LANDED:  " + damage + " DAMAGE!");
                animator.SetTrigger("Damaged");

                cs.health -= damage;

                if (isFaceRight)
                {
                    knockbackDirection = 1f;
                }
                else
                {
                    knockbackDirection = -1f;
                }

                if (isPlayer)
                {
                    cs.invFrames = 50;
                }
            }
        }
    }
}
