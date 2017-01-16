using UnityEngine;
using System.Collections;

public class ShotController : MonoBehaviour {
    

    public void setScale(bool isRight, float height)
    {
        GameObject spriteChild = transform.FindChild("SuperShotSprite").gameObject;
        spriteChild.transform.localPosition = new Vector3(0,height);
        if (isRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
            GetComponent<Rigidbody2D>().velocity = new Vector2(10f, 0f);
        }
        else
        {
            spriteChild.GetComponent<SpriteRenderer>().flipX = true;
            transform.localScale = new Vector3(1, 1, 1);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-10f, 0f);
        }
    }

    // Use this for initialization
    void Start () {
        GameCommands.ResetDepth();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(transform.position.x > 50f)
        {
            Destroy(gameObject);
        }
        else if(transform.position.x < -50f)
        {
            Destroy(gameObject);
        }
    }
}
