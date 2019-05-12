using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    public void Walk(bool left)
    {
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
    }
}
