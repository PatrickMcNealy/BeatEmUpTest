using UnityEngine;
using System.Collections;

public class debris : MonoBehaviour {

    float fallspeed = 50f;
    float xDeviation = 5f;

    Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        fallspeed -= 5f;
        rb2d.velocity = new Vector2(xDeviation, fallspeed);

        if(rb2d.transform.position.y < -100f)
        {
            Destroy(this.gameObject);
        }
	}
}
