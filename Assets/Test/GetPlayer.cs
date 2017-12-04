using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayer : MonoBehaviour
{

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        GameObject Player1 = GameObject.FindGameObjectWithTag("Player1");
        GameObject Player2 = GameObject.FindGameObjectWithTag("Player2");

        if (Player1.GetComponent<PlayerPoint>().myPoint > Player2.GetComponent<PlayerPoint>().myPoint)
        {
            Player1.GetComponent<PlayerPoint>().PlayerRank = 0;
            Player2.GetComponent<PlayerPoint>().PlayerRank = 1;
        }
        if (Player1.GetComponent<PlayerPoint>().myPoint < Player2.GetComponent<PlayerPoint>().myPoint)
        {
            Player1.GetComponent<PlayerPoint>().PlayerRank = 1;
            Player2.GetComponent<PlayerPoint>().PlayerRank = 0;
        }
    }
}
