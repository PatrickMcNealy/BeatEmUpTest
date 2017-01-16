using UnityEngine;
using System.Collections;

public class MetDmgBox : MonoBehaviour {

    Animator anim;
    public GameObject groundGO;
    Transform groundTransform;
    static SpriteBase XSprite;
    // Use this for initialization
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        groundTransform = groundGO.GetComponent<Transform>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(XSprite == null)
        {
            XSprite = other.GetComponent<SpriteBase>();
        }
        Debug.Log("METHIT!");
        //SpriteBase asdf = other.GetComponent<SpriteBase>();
        XSprite.OnHit(true, 2, groundTransform.position.y);
    }

}
