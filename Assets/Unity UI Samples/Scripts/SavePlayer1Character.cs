using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePlayer1Character : MonoBehaviour
{

    public GameObject p1;
    
    void Start() {
        p1 = GameObject.Find("Canvas/Player1Panel/Player1DropBox/Player1DropImage");
    }

    void OnDisable()
    {
        PlayerPrefs.SetString("p1c",
                p1.GetComponent<Image>().overrideSprite.name);
    }
}
