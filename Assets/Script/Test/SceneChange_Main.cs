using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//=======================================================
//残り時間が0になったかを取得して画面遷移を行うスクリプト
//=======================================================

public class SceneChange_Main : MonoBehaviour
{
    //TimeCountスクリプト参照用変数
    public TimeCount timeCount;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //フラグがtrueなら遷移を開始
        if (timeCount.TimeUpFlag == true)
        {
            SceneManager.LoadScene("Result");
        }
	}

}
