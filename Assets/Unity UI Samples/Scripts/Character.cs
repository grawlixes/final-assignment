using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public string characterName;
    public string state;
    public int stateLength;
    public int nextUpdate;
    public int player;
    public int animationIndex;
    public int health;
    public GameObject self;

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

        health = 100;
       
        self = GameObject.Find("Canvas/Player" + player.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        nextUpdate -= 1;

        if (nextUpdate == 0) {
            // build path to next image and use it
            string img = characterName + '_' + "idle" + '_' +
                         (animationIndex + 1).ToString();
            self.GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>(img);
           
            animationIndex = (animationIndex + 1) % stateLength;

            nextUpdate = FREQ;
                
            if (state == "left") {
                self.transform.localPosition -= new Vector3(20F, 0, 0);
                nextUpdate = 5;
            } else if (state == "right") {
                self.transform.localPosition += new Vector3(20F, 0, 0);
                nextUpdate = 5;
            }
        }
    }

    // Changes state.
    void ChangeState(string newState, int newStateLength) {
        state = newState;
        stateLength = newStateLength;
        animationIndex = 0;
    }
}
