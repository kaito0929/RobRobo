using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public MainEnd mainEnd;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (mainEnd.EndFlag == true)
        {
            SceneManager.LoadScene("Result");
        }
	}

}
