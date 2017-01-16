using UnityEngine;
using System.Collections;

public class ShotSpawner : MonoBehaviour {

    public GameObject superBlast;

    public GameObject parent;
    Transform groundTransform;


	// Use this for initialization
	void Start () {
        groundTransform = parent.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SuperBlast()
    {
        GameObject blast = (GameObject)Instantiate(superBlast);
        
        blast.GetComponent<Rigidbody2D>().transform.position = new Vector3(groundTransform.position.x, groundTransform.position.y, groundTransform.position.z);
        bool direction = (!GetComponent<SpriteRenderer>().flipX);

        blast.GetComponent<ShotController>().setScale(direction, transform.localPosition.y);
    }
}
