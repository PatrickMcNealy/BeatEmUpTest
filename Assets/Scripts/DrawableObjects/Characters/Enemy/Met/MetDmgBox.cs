using UnityEngine;
using System.Collections;

public class MetDmgBox : MonoBehaviour
{

    public GameObject groundGO;
    Transform groundTransform;
    static CharacterBase XSprite;

    // Use this for initialization
    void Start()
    {
        groundTransform = groundGO.GetComponent<Transform>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (XSprite == null)
        {
            XSprite = other.GetComponent<CharacterBase>();
        }
        //Debug.Log("METHIT!");
        //SpriteBase asdf = other.GetComponent<SpriteBase>();
        XSprite.OnHit(true, 2, groundTransform.position.y);
    }
}
