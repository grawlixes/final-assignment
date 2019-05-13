using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Character : MonoBehaviour
{
    public string characterName;
    public string state;
    public int stateLength;
    public int nextUpdate;
    public int player;
    public int animationIndex;
    public int health;
    public int touches;
    public bool facingForward;

    public GameObject self;
    public Move leftButton;
    public Move rightButton;

    public const int FREQ = 30;

    // Start is called before the first frame update
    void Start()
    {
        // Get character name from the extra.
        characterName = PlayerPrefs
                       .GetString("p" + player.ToString() + "c");
        characterName = characterName
                        .Substring(0, characterName.IndexOf("I"));
        
        state = "idle";
        stateLength = 2;
        nextUpdate = FREQ;
        animationIndex = 0;
        facingForward = player == 1;

        health = 100;
    
        self = GameObject.Find("Canvas/Player" + player.ToString());
        
        leftButton = GameObject.Find("Canvas/LeftButton")
                               .GetComponent<Move>();
        rightButton = GameObject.Find("Canvas/RightButton")
                               .GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        nextUpdate -= 1;
        touches = Math.Max(touches, this.NumTouches());

        if (nextUpdate == 0 || (player == 1 && touches > 0)) {
            // build path to next image and use it
            string img = characterName + '_' + state + '_' +
                         (animationIndex + 1).ToString();
            self.GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>(img);
           
            animationIndex = (animationIndex + 1) % stateLength;
            nextUpdate = FREQ;

            if (animationIndex == 0 && touches == 0 &&
                ((state.Length >= 5 && state.Substring(0, 5) == "light") ||
                 state == "heavy" || state == "upper")) {
                state = "idle";
                stateLength = 2;
                nextUpdate = 1;
            }
           
            if (player == 1 && 
                    (animationIndex == stateLength-1 || state == "idle") &&
                    touches == 1) {
                float tapPosition = this.GetTouch(0);

                if (facingForward ^
                    tapPosition > self.transform.position.x) {
                    self.transform.localScale = 
                        new Vector3(self.transform.localScale.x * -1,
                                    self.transform.localScale.y,
                                    self.transform.localScale.z);
                    facingForward = !facingForward;
                }

                if (state == "idle" || state == "walking") {
                    state = "light1";
                    stateLength = 3;
                    nextUpdate = 1;
                    animationIndex = 0;
                    
                    int mul = -1;
                    if (facingForward)
                        mul = 1;
                    
                    self.transform.localPosition += new Vector3(10F * mul, 0, 0);
                } else if (state == "light1") {
                    state = "light2";
                    stateLength = 4;
                    nextUpdate = 1;
                    animationIndex = 0;
                    
                    int mul = -1;
                    if (facingForward)
                        mul = 1;
                    
                    self.transform.localPosition += new Vector3(20F * mul, 0, 0);
                } else if (state == "light2") {
                    state = "light2";
                    stateLength = 4;
                    nextUpdate = 1;
                    animationIndex = 0;
                    
                    int mul = -1;
                    if (facingForward)
                        mul = 1;
                    
                    self.transform.localPosition += new Vector3(20F * mul, 0, 0);
                } else if (state == "light3") {
                    // Heavy attack
                }

                touches = 0;
                nextUpdate = 1;
            } else if (state == "light1") {
                if (animationIndex == 2)
                    nextUpdate = 16;
                else
                    nextUpdate = 8;
            } else if (state == "light2") {
                if (animationIndex == 3)
                    nextUpdate = 16;
                else
                    nextUpdate = 4;
            } else if (player == 1 &&
                    NumTouches() >= 2) {
                // TODO blocking, only works on mobile
                // because you can't double tap on PC.

                touches = 0;
                nextUpdate = 1;
            } else if (player == 1 &&
                    state == "walking" &&
                    leftButton.isPressed) {
                self.transform.localPosition -= new Vector3(35F, 0, 0);
                nextUpdate = 9;
                if (facingForward) {
                    self.transform.localScale = 
                        new Vector3(self.transform.localScale.x * -1,
                                    self.transform.localScale.y,
                                    self.transform.localScale.z);
                    facingForward = false;
                }
            } else if (player == 1 &&
                       state == "walking" &&
                       rightButton.isPressed) {
                self.transform.localPosition += new Vector3(35F, 0, 0);
                nextUpdate = 9;
                if (!facingForward) {
                    self.transform.localScale =
                        new Vector3(self.transform.localScale.x * -1,
                                    self.transform.localScale.y,
                                    self.transform.localScale.z);
                    facingForward = true;
                }
            }
        }
    }

    int NumTouches() {
        if (Application.platform == RuntimePlatform.WindowsEditor) {
            if (Input.GetMouseButtonDown(0) &&
                    !leftButton.isPressed && !rightButton.isPressed) {
                return 1;
            }
            return 0;
        } else {
           return Input.touchCount;
        } 
    }

    float GetTouch(int touchIndex) {
        if (Application.platform == RuntimePlatform.WindowsEditor) {
            Vector3 ret = Input.mousePosition;
            ret.z = 100;
            return Camera.main.ScreenToWorldPoint(ret).x;
        } else {
            return Input.GetTouch(touchIndex).position.x;
        } 
    }
    // Changes state.
    public void ChangeState(string newState, int newStateLength) {
        state = newState;
        stateLength = newStateLength;
        animationIndex = 0;
    }
}
