using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecreaseHealth : MonoBehaviour
{
    public int player;

    public void Run(int checksum) {
        int damage = Mathf.Abs(checksum);

        GameObject bar;
        GameObject health;
        if (player == 1) {
            bar = GameObject.Find("Canvas/Player1Health");
            health = GameObject.Find("Canvas/Player1Health/Health1");
        } else {
            bar = GameObject.Find("Canvas/Player2Health");
            health = GameObject.Find("Canvas/Player2Health/Health2");
        }
        
        Vector3 pos = bar.transform.position;
        health.transform.localScale -= new Vector3((float) damage/100, 0, 0);
        
        Vector3 pos2 = health.transform.position;
        float change = (float) (player * (.35 * ((float) damage/100)));
        Debug.Log(change);
        health.transform.localPosition -= new Vector3(change, 0, 0);
    }

}
