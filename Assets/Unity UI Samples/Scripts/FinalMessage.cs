using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalMessage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI message = GameObject.Find("Canvas/FinalWindow/FinalText")
                                            .GetComponent<TextMeshProUGUI>();

        string loser = PlayerPrefs.GetString("loser");
        string[] funnyWord = {"CULLED", "DESTROYED", "BODIED", "THRASHED", 
                            "ANNIHILATED", "BEATEN DOWN", "WRECKED",
                            "BULLIED", "WHACKED", "REKT"};

        string newMessage = "PLAYER " + loser + " WAS\n" + 
                            funnyWord[Random.Range(0, 10)] + ".";
        message.text = newMessage;
    }
}
