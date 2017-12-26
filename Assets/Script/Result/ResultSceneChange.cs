using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//==================================================
//リザルト画面でタイトルへ遷移するスクリプト
//==================================================

public class ResultSceneChange : MonoBehaviour
{
    // 変数宣言---------------------------------------------------------------------------------------------

    //ボタンを押しているかのフラグ
    private bool ButtonPushFlag;

    //フェードイン、フェードアウトを行うためのUI
    public Image FadeImage;
    //α値
    public float Alfa;
    //フェードイン、フェードアウトのスピード
    private float FadeSpeed;

    // 初期化---------------------------------------------------------------------------------------------
    void Start ()
    {
        Alfa = 1.0f;
        FadeSpeed = 0.01f;
        ButtonPushFlag = false;
	}

    // Update is called once per frame
    void Update()
    {
        //コントローラーの○ボタンを押したら処理
        if (Input.GetButton("Circle"))
        {
            if (Alfa <= 0.0f)
            {
                //フラグがfalse、つまりはボタンを一度も押していない場合なら処理される
                if (ButtonPushFlag == false)
                {
                    //フラグをtrueに
                    ButtonPushFlag = true;
                }
            }
        }

        //フェードアウトの処理
        //ボタンを押して、α値が0.0f以下なら処理
        if (ButtonPushFlag == true && Alfa <= 1.0f)
        {
            //α値を加算してImageの透明度を変化
            Alfa += FadeSpeed;
        }
        //フェードインの処理
        //ボタンが押されてないと処理される
        else if(ButtonPushFlag == false && Alfa >= 0.0f)
        {
            Alfa -= FadeSpeed;
        }

        //フェードアウトが終了したら処理
        if (ButtonPushFlag == true && Alfa >= 1.0f)
        {
            //Photonとの接続を切る
            PhotonNetwork.Disconnect();
            //タイトル画面へ遷移
            SceneManager.LoadScene("Title");
        }

        //フェード用のImageのα値を渡す
        FadeImage.color = new Color(0.0f, 0.0f, 0.0f, Alfa);
    }

}
