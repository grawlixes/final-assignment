using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public bool[] isPressed;
    public int button = 0;
    public Character p1;
    public Character p2;

    void Start() {
        isPressed = new bool[2];
        isPressed[0] = false;
        isPressed[1] = false;
        p1 = GameObject.Find("/Canvas/Player1")
                       .GetComponent<Character>();
        p2 = GameObject.Find("/Canvas/Player2")
                       .GetComponent<Character>();
    }

    void Update() {
        if (Input.GetKeyUp("a") && button == 1) {
            onPointerUpP1();
        } else if (Input.GetKeyDown("a") && button == 1) {
            onPointerDownP1();
        } else if (Input.GetKeyUp("d") && button == 2) {
            onPointerUpP1();
        } else if (Input.GetKeyDown("d") && button == 2) {
            onPointerDownP1();
        }
        
        if (Input.GetKeyUp("j") && button == 1) {
            onPointerUpP2();
        } else if (Input.GetKeyDown("j") && button == 1) {
            onPointerDownP2();
        } else if (Input.GetKeyUp("l") && button == 2) {
            onPointerUpP2();
        } else if (Input.GetKeyDown("l") && button == 2) {
            onPointerDownP2();
        }
    }

    public void onPointerDownP1()
    {
        p1.ChangeState("walking", 3);
        p1.nextUpdate = 1;
        isPressed[0] = true;
    }
    
    public void onPointerUpP1()
    {
        p1.ChangeState("idle", 2);
        p1.nextUpdate = 1;
        isPressed[0] = false;
    }
    
    public void onPointerDownP2()
    {
        p2.ChangeState("walking", 3);
        p2.nextUpdate = 1;
        isPressed[1] = true;
    }
    
    public void onPointerUpP2()
    {
        p2.ChangeState("idle", 2);
        p2.nextUpdate = 1;
        isPressed[1] = false;
    }
}
