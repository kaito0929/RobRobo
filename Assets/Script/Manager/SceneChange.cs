using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//==================================================
//指定した人数が集まった場合にシーンを遷移させる
//==================================================

public class SceneChange : MonoBehaviour
{    
    // 変数宣言---------------------------------------------------------------------------------------------
    //GameStartへの参照
    //アタッチされているオブジェクトは別なのでInspectorで設定するように
    public GameStart gameStart;
	
	// Update is called once per frame
	void Update ()
    {
        //GameStartで宣言したPlayerNecessaryNumの数値が
        //0以下、つまり指定人数集まった場合に画面遷移を開始
        if (gameStart.GetPlayerNecessaryNum() <= 0)
        {
            SceneManager.LoadScene("Main");
        }
    }
}
