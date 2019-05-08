using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string p1c = PlayerPrefs.GetString("p1c");
        GameObject p1i = GameObject.Find("Canvas/Player1Image");
        p1i.GetComponent<Image>().sprite =
            Resources.Load<Sprite>(p1c);

        string p2c = PlayerPrefs.GetString("p2c");
        GameObject p2i = GameObject.Find("Canvas/Player2Image");
        p2i.GetComponent<Image>().sprite =
            Resources.Load<Sprite>(p2c);
    }

    // Update is called once per frame
}
