using UnityEngine;
using System.Collections;

public class MegamanShotDmgBox : MonoBehaviour {

    public GameObject groundGO;
    Transform groundTransform;
    SpriteRenderer sprite;

    // Use this for initialization
    void Start () {
        groundTransform = groundGO.GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
	    //if(transform.lossyScale.x > 0)
     //   {
     //       groundGO.GetComponent<Rigidbody2D>().velocity = new Vector2(10f, 0f);
     //   }
     //   else
     //   {
     //       groundGO.GetComponent<Rigidbody2D>().velocity = new Vector2(-10f, 0f);
     //   }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!sprite.flipX)
        {
            other.GetComponent<SpriteBase>().OnHit(true, 20, groundTransform.position.y);
        }
        else
        {
            other.GetComponent<SpriteBase>().OnHit(false, 20, groundTransform.position.y);
        }
    }
}