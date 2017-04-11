using UnityEngine;
using System.Collections;

public class MegamanDmgBox : MonoBehaviour {

    Animator anim;
    public GameObject groundGO;
    Transform groundTransform;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponentInParent<Animator>();
        groundTransform = groundGO.GetComponent<Transform>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("SwordSlash"))
        {
            if (transform.lossyScale.x > 0)
            {
                other.GetComponent<CharacterBase>().OnHit(true, 10, groundTransform.position.y);
            }
            else
            {
                other.GetComponent<CharacterBase>().OnHit(false, 10, groundTransform.position.y);
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Punch1"))
        {
            if (transform.lossyScale.x > 0)
            {
                other.GetComponent<CharacterBase>().OnHit(true, 5, groundTransform.position.y);
            }
            else
            {
                other.GetComponent<CharacterBase>().OnHit(false, 5, groundTransform.position.y);
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Punch2"))
        {
            if (transform.lossyScale.x > 0)
            {
                other.GetComponent<CharacterBase>().OnHit(true, 5, groundTransform.position.y);
            }
            else
            {
                other.GetComponent<CharacterBase>().OnHit(false, 5, groundTransform.position.y);
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("AiroSlash"))
        {
            if (transform.lossyScale.x > 0)
            {
                other.GetComponent<CharacterBase>().OnHit(true, 10, groundTransform.position.y);
            }
            else
            {
                other.GetComponent<CharacterBase>().OnHit(false, 10, groundTransform.position.y);
            }
        }

    }
}