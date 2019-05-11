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
            // build path to next image
            string img = characterName + '_' + state + '_' +
                         (animationIndex + 1).ToString();
            self.GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>(img);
           
            animationIndex = (animationIndex + 1) % stateLength;

            nextUpdate = FREQ;
        }
    }
}
