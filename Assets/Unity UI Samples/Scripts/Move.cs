using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public bool isPressed = false;
    public int button = 0;
    public Character p1;
    
    // Start is called before the first frame update
    void Start() {
        p1 = GameObject.Find("/Canvas/Player1")
                       .GetComponent<Character>();
    }

    void Update() {
        if (Input.GetKeyUp("a") && button == 1) {
            onPointerUp();
        } else if (Input.GetKeyDown("a") && button == 1) {
            onPointerDown();
        } else if (Input.GetKeyUp("d") && button == 2) {
            onPointerUp();
        } else if (Input.GetKeyDown("d") && button == 2) {
            onPointerDown();
        }
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
