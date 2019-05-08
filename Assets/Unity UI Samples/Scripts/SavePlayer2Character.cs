using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePlayer2Character : MonoBehaviour
{

    public GameObject p2;
    
    void Start() {
        p2 = GameObject.Find("Canvas/Player2Panel/Player2DropBox/Player2DropImage");
    }

    void OnDisable()
    {
        PlayerPrefs.SetString("p2c",
                p2.GetComponent<Image>().overrideSprite.name);
    }
}
