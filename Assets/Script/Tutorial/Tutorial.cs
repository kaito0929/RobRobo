using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public Image TutorialImage;
    public Sprite[] TutorialSprite = new Sprite[5];

    private int DisplayNum;

    private bool PushFlag;

    public AudioSource sound01;


    // Use this for initialization
    void Start ()
    {
        DisplayNum = 0;
        PushFlag = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetButton("Circle"))
        {
            if (PushFlag == false && DisplayNum < 4)
            {
                sound01.PlayOneShot(sound01.clip);
                DisplayNum++;
                PushFlag = true;
            }

            if (PushFlag == false && DisplayNum == 4)
            {
                sound01.PlayOneShot(sound01.clip);
                SceneManager.LoadScene("Title");
            }

        }
        else
        {
            PushFlag = false;
        }

        TutorialImage.sprite = TutorialSprite[DisplayNum];
	}
}
