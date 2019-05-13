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

    public void onPointerDown()
    {
        p1.ChangeState("walking", 3);
        p1.nextUpdate = 1;
        isPressed = true;
    }
    
    public void onPointerUp()
    {
        p1.ChangeState("idle", 2);
        p1.nextUpdate = 1;
        isPressed = false;
    }
}
