using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

    public GameObject target;

    float movementThresholdX = 3f;
    

    // Update is called once per frame
    void Update() {

        //Track player's X position, with room for leneance so it isn't constantly moving for everything.
        if (target.transform.position.x < transform.position.x - movementThresholdX)
        {
            transform.position = new Vector3(target.transform.position.x + movementThresholdX, transform.position.y, transform.position.z);
        }
        else if (target.transform.position.x > transform.position.x + movementThresholdX)
        {
            transform.position = new Vector3(target.transform.position.x - movementThresholdX, transform.position.y, transform.position.z);
        }
    }
}
