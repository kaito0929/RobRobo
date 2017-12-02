using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//===================================================
//タイトルからマッチングルームへ遷移する
//===================================================

public class TitleSceneChange : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("test");
        }
	}
}
