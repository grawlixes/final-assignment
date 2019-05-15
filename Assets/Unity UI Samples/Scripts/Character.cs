using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Character : MonoBehaviour
{
    public string characterName;
    public string state;
    public string swipe;
    public int stateLength;
    public int nextUpdate;
    public int player;
    public int animationIndex;
    public int health;
    public int touches;
    public int hitFrom;
    public bool facingForward;

    public GameObject self;
    public GameObject opponent;
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
        swipe = "";
        stateLength = 2;
        nextUpdate = FREQ;
        animationIndex = 0;
        facingForward = (player == 1);
        touches = 0;
        hitFrom = 0;

        health = 100;
    
        self = GameObject.Find("Canvas/Player" + player.ToString());
        opponent = GameObject.Find("Canvas/Player" + (3-player).ToString());

        leftButton = GameObject.Find("Canvas/LeftButton")
                               .GetComponent<Move>();
        
        rightButton = GameObject.Find("Canvas/RightButton")
                               .GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) {
            PlayerPrefs.SetString("loser", player.ToString());
            SceneManager.LoadScene("winnerScene");
        }

        nextUpdate -= 1;
        if (touches == 0) 
            touches = this.NumTouches();

        if (nextUpdate == 0 || touches != 0) {
            // build path to next image and use it
            string img = characterName + '_' + state + '_' +
                         (animationIndex + 1).ToString();
            self.GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>(img);
           
            animationIndex = (animationIndex + 1) % stateLength;
            nextUpdate = FREQ;

            //Debug.Log("Player " 

            if (animationIndex == 0 && touches == 0 &&
                state != "idle" && state != "walking") {
                state = "idle";
                stateLength = 2;
                nextUpdate = 8;
            } else if ( 
                    (animationIndex == 0 || state == "idle") &&
                    touches == -1) {
                Debug.Log("Jump (up swipe)");
                touches = 0;
            } else if (
                    (animationIndex == 0 || state == "idle") &&
                    touches == -2) {
                state = "heavy";
                stateLength = 5;
                nextUpdate = 1;
                animationIndex = 0;
                touches = 0;
            } else if ( 
                    (animationIndex == 0 || state == "idle") &&
                    touches == -3) {
                Debug.Log("upper attack (down swipe)");
                touches = 0;
            } else if (  
                    (animationIndex == 0 || state == "idle") &&
                    touches == -4) {
                // dodge
                int mul = 1;
                if (facingForward)
                    mul = -1;
                
                state = "dodge";
                stateLength = 3;
                animationIndex = 0;
                nextUpdate = 1;
                touches = 0;

                self.transform.localPosition += new Vector3(5F * mul, 0, 0);
                if (self.transform.localPosition.x < -500) {
                    self.transform.localPosition = new Vector3(-500,
                            self.transform.localPosition.y,
                            self.transform.localPosition.z);
                }
                
                if (self.transform.localPosition.x > 500) {
                    self.transform.localPosition = new Vector3(500,
                            self.transform.localPosition.y,
                            self.transform.localPosition.z);
                }
            } else if ( 
                    (animationIndex == 0 || state == "idle") &&
                    touches == 1) {

                /*
                float tapPosition = this.GetTouch(0);
                
                if (facingForward ^
                    tapPosition > self.transform.position.x) {
                    self.transform.localScale = 
                        new Vector3(self.transform.localScale.x * -1,
                                    self.transform.localScale.y,
                                    self.transform.localScale.z);
                    facingForward = !facingForward;
                }
                */

                if (state == "idle" || state == "walking") {
                    state = "light1";
                    stateLength = 3;
                    nextUpdate = 1;
                    animationIndex = 0;
                    
                    int mul = -1;
                    if (facingForward)
                        mul = 1;
                    
                    self.transform.localPosition += new Vector3(10F * mul, 0, 0);
                    if (self.transform.localPosition.x < -500) {
                        self.transform.localPosition = new Vector3(-500,
                                self.transform.localPosition.y,
                                self.transform.localPosition.z);
                    }
                    
                    if (self.transform.localPosition.x > 500) {
                        self.transform.localPosition = new Vector3(500,
                                self.transform.localPosition.y,
                                self.transform.localPosition.z);
                    }
                } else if (state == "light1") {
                    state = "light2";
                    stateLength = 4;
                    nextUpdate = 1;
                    animationIndex = 0;
                    
                    int mul = -1;
                    if (facingForward)
                        mul = 1;
                    
                    self.transform.localPosition += new Vector3(20F * mul, 0, 0);
                    if (self.transform.localPosition.x < -500) {
                        self.transform.localPosition = new Vector3(-500,
                                self.transform.localPosition.y,
                                self.transform.localPosition.z);
                    }
                    
                    if (self.transform.localPosition.x > 500) {
                        self.transform.localPosition = new Vector3(500,
                                self.transform.localPosition.y,
                                self.transform.localPosition.z);
                    }
                } else if (state == "light2") {
                    state = "light3";
                    stateLength = 4;
                    nextUpdate = 1;
                    animationIndex = 0;
                } else if (state == "light3") {
                    state = "heavy";
                    stateLength = 5;
                    nextUpdate = 1;
                    animationIndex = 0;
                } else {
                    // ignore it
                    nextUpdate = 1;
                }

                touches = 0;
            } else if (state == "stun") {
                if (animationIndex == 1) {
                    nextUpdate = 20;
                } else {
                    nextUpdate = 8;
                }

                int mul = 1;
                if (facingForward) mul = -1;
                self.transform.localPosition += new Vector3(10F * mul, 0, 0);
                
                if (self.transform.localPosition.x < -500) {
                    self.transform.localPosition = new Vector3(-500,
                            self.transform.localPosition.y,
                            self.transform.localPosition.z);
                }
                
                if (self.transform.localPosition.x > 500) {
                    self.transform.localPosition = new Vector3(500,
                            self.transform.localPosition.y,
                            self.transform.localPosition.z);
                }
            } else if (state == "back") {
                if (animationIndex == 2) {
                    nextUpdate = 8;
                
                    int mul2 = 1;
                    if (facingForward) mul2 = -1;
                    self.transform.localPosition += new Vector3(80F * mul2, 0, 0);
                    if (self.transform.localPosition.x < -500) {
                        self.transform.localPosition = new Vector3(-500,
                                self.transform.localPosition.y,
                                self.transform.localPosition.z);
                    }
                    
                    if (self.transform.localPosition.x > 500) {
                        self.transform.localPosition = new Vector3(500,
                                self.transform.localPosition.y,
                                self.transform.localPosition.z);
                    }
                } else if (animationIndex == 3) {
                    nextUpdate = 120;
                } else {
                    nextUpdate = 8;
                }

                int mul = 1;
                if (facingForward) mul = -1;
                self.transform.localPosition += new Vector3(30F * mul, 0, 0);
                if (self.transform.localPosition.x < -500) {
                    self.transform.localPosition = new Vector3(-500,
                            self.transform.localPosition.y,
                            self.transform.localPosition.z);
                }
                
                if (self.transform.localPosition.x > 500) {
                    self.transform.localPosition = new Vector3(500,
                            self.transform.localPosition.y,
                            self.transform.localPosition.z);
                }
            } else if (state == "light1") {
                if (animationIndex == 2) {
                    if ((self.transform.localPosition.x <
                            opponent.transform.localPosition.x ^ !facingForward)
                            && Math.Abs(self.transform.localPosition.x - 
                                 opponent.transform.localPosition.x) < 100)
                        this.DealDamage(2, "");
                    nextUpdate = 16;
                } else {
                    nextUpdate = 8;
                }
            } else if (state == "light2") {
                if (animationIndex == 3) {
                    if ((self.transform.localPosition.x <
                            opponent.transform.localPosition.x ^ !facingForward)
                            && Math.Abs(self.transform.localPosition.x - 
                                 opponent.transform.localPosition.x) < 120)
                        this.DealDamage(2, "");
                    nextUpdate = 16;
                } else {
                    nextUpdate = 8;
                }
            } else if (state == "light3") {
                if (animationIndex == 2) {
                    if ((self.transform.localPosition.x <
                            opponent.transform.localPosition.x ^ !facingForward)
                            && Math.Abs(self.transform.localPosition.x - 
                                 opponent.transform.localPosition.x) < 120)
                        this.DealDamage(3, "");
                    nextUpdate = 20;
                } else {
                    nextUpdate = 8;
                }
            } else if (state == "heavy") {
                if (animationIndex == 2) {
                    if ((self.transform.localPosition.x <
                            opponent.transform.localPosition.x ^ !facingForward)
                            && Math.Abs(self.transform.localPosition.x - 
                                 opponent.transform.localPosition.x) < 150)
                        this.DealDamage(5, "back");
                    nextUpdate = 16;
                } else {
                    nextUpdate = 8;
                }
            } else if (state == "dodge") {
                nextUpdate = 4;
                
                int mul = 1;
                if (facingForward) mul = -1;

                self.transform.localPosition += new Vector3(80F * mul, 0, 0);
                if (self.transform.localPosition.x < -500) {
                    self.transform.localPosition = new Vector3(-500,
                            self.transform.localPosition.y,
                            self.transform.localPosition.z);
                }
                
                if (self.transform.localPosition.x > 500) {
                    self.transform.localPosition = new Vector3(500,
                            self.transform.localPosition.y,
                            self.transform.localPosition.z);
                }
            } else if (
                    NumTouches() >= 2) {
                // TODO blocking, only works on mobile
                // because you can't double tap on PC.

                touches = 0;
                nextUpdate = 1;
            } else if (
                    state == "walking" &&
                    leftButton.isPressed[player-1]) {
                Debug.Log("Player " + player + " walking left");
                self.transform.localPosition -= new Vector3(35F, 0, 0);
                if (self.transform.localPosition.x < -500) {
                    self.transform.localPosition = new Vector3(-500,
                            self.transform.localPosition.y,
                            self.transform.localPosition.z);
                }
                
                if (self.transform.localPosition.x > 500) {
                    self.transform.localPosition = new Vector3(500,
                            self.transform.localPosition.y,
                            self.transform.localPosition.z);
                }
                nextUpdate = 9;
                if (facingForward) {
                    self.transform.localScale = 
                        new Vector3(self.transform.localScale.x * -1,
                                    self.transform.localScale.y,
                                    self.transform.localScale.z);
                    facingForward = false;
                }
            } else if (
                       state == "walking" &&
                       rightButton.isPressed[player-1]) {
                Debug.Log("Player " + player + " walking right");
                self.transform.localPosition += new Vector3(35F, 0, 0);
                if (self.transform.localPosition.x < -500) {
                    self.transform.localPosition = new Vector3(-500,
                            self.transform.localPosition.y,
                            self.transform.localPosition.z);
                }
                
                if (self.transform.localPosition.x > 500) {
                    self.transform.localPosition = new Vector3(500,
                            self.transform.localPosition.y,
                            self.transform.localPosition.z);
                }
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

    // If ret > 0, then there were ret touches.
    // If ret == 0, then there were no touches.
    // If ret < 0, then there was a swipe.
    // (-1, -2, -3, -4) -> (jump, heavy, upper, roll).
    int NumTouches() {
        if (Application.platform == RuntimePlatform.WindowsEditor) {
            if (player == 1) {
                if (Input.GetKeyDown("e")) {
                    return 1;
                } else if (Input.GetKeyDown("space")) {
                    return -1;
                } else if (Input.GetKeyDown("f")) {
                    return -2;
                } else if (Input.GetKeyDown("q")) {
                    return -3;
                } else if (Input.GetKeyDown("left shift")) {
                    return -4;
                }
            } else {
                if (Input.GetKeyDown("u")) {
                    return 1;
                } else if (Input.GetKeyDown("n")) {
                    return -1;
                } else if (Input.GetKeyDown("h")) {
                    return -2;
                } else if (Input.GetKeyDown("o")) {
                    return -3;
                } else if (Input.GetKeyDown("right shift")) {
                    return -4;
                }
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

    // Effect: "back" - flying back
    //         "up"   - flying up
    public void DealDamage(int damage, string effect) {
        int mul = 1;
        if (player == 1) mul = -1;

        opponent.GetComponent<DecreaseHealth>().Run(mul * damage);
        Character enemy = opponent.GetComponent<Character>();
        enemy.health -= damage;

        if (effect == "") {
            enemy.state = "stun";
            enemy.stateLength = 2;
            enemy.animationIndex = 0;
            enemy.nextUpdate = 1;
            if (!(enemy.facingForward ^ facingForward)) {
                opponent.transform.localScale = new Vector3(
                        opponent.transform.localScale.x * -1,
                        opponent.transform.localScale.y,
                        opponent.transform.localScale.z);
                enemy.facingForward = !enemy.facingForward;
            }
        } else if (effect == "back") {
            enemy.state = "back";
            enemy.stateLength = 3;
            enemy.animationIndex = 0;
            enemy.nextUpdate = 1;
            if (!(enemy.facingForward ^ facingForward)) {
                opponent.transform.localScale = new Vector3(
                        opponent.transform.localScale.x * -1,
                        opponent.transform.localScale.y,
                        opponent.transform.localScale.z);
                enemy.facingForward = !enemy.facingForward;
            }
        }
    }
}
