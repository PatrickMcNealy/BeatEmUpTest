using UnityEngine;
using System.Collections;

public class MegamanShotDmgBox : MonoBehaviour
{

    public GameObject groundGO;
    Transform groundTransform;
    SpriteRenderer sprite;

    // Use this for initialization
    void Start()
    {
        groundTransform = groundGO.GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!sprite.flipX)
        {
            other.GetComponent<CharacterBase>().OnHit(true, 20, groundTransform.position.y);
        }
        else
        {
            other.GetComponent<CharacterBase>().OnHit(false, 20, groundTransform.position.y);
        }
    }
}