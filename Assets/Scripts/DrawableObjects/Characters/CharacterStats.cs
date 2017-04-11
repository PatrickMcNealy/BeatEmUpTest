using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour {

    public int invFrames = 0;
    public int lives = 0;
    public int maxHealth = 1;
    public int health = 1;

    public void FixedUpdate()
    {
        if(invFrames > 0)
        {
            invFrames--;
        }
    }
}
