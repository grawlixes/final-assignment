using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public bool isPressed = false;
    public Character p1;
    
    // Start is called before the first frame update
    void Start() {
        p1 = GameObject.Find("/Canvas/Player1")
                       .GetComponent<Character>();
    }

    public void Walk(bool left)
    {
    /*
        Character p1 = GameObject.Find("/Canvas/Player1")
                                 .GetComponent<Character>();
        if (left) {
            if (p1.state == "left") 
                p1.state = "idle";
            else
                p1.state = "left";
        } else {
            if (p1.state == "right") 
                p1.state = "idle";
            else
                p1.state = "right";
        }

        p1.nextUpdate = 1;
    */
    }

    public void onPointerDown()
    {
        isPressed = true;
        p1.nextUpdate = 1;
    }
    
    public void onPointerUp()
    {
        isPressed = false;
        p1.nextUpdate = 1;
    }
}
