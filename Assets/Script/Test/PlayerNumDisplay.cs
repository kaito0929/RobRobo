using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNumDisplay : MonoBehaviour
{
    public Text text;
    public GameStart gameStart;

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        text.text = "後" + gameStart.GetPlayerNecessaryNum() + "人";
    }
}
