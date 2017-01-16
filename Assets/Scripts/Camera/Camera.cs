using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

    public GameObject target;

    float movementThresholdX = 3f;
    float movementThresholdY = 2f;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update() {
        if (target.transform.position.x < transform.position.x - movementThresholdX)
        {
            transform.position = new Vector3(target.transform.position.x + movementThresholdX, transform.position.y, transform.position.z);
        }
        else if (target.transform.position.x > transform.position.x + movementThresholdX)
        {
            transform.position = new Vector3(target.transform.position.x - movementThresholdX, transform.position.y, transform.position.z);
        }


        if (target.transform.position.y < transform.position.y - movementThresholdY)
        {
            transform.position = new Vector3(transform.position.x, target.transform.position.y + movementThresholdY, transform.position.z);
        }
        else if (target.transform.position.y > transform.position.y + movementThresholdY)
        {
            transform.position = new Vector3(transform.position.x, target.transform.position.y - movementThresholdY , transform.position.z);
        }
    }
}
