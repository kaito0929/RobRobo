using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//=======================================================
//残り時間が0になったかを取得して画面遷移を行うスクリプト
//=======================================================

public class SceneChange_Main : MonoBehaviour
{    
    // 変数宣言---------------------------------------------------------------------------------------------
    //TimeCountスクリプト参照用変数
    public TimeCount timeCount;

    //フェードイン、フェードアウトを行うためのUI
    public Image FadeImage;
    //α値
    public float Alfa;
    //フェードイン、フェードアウトのスピード
    private float FadeSpeed;


    // 初期化---------------------------------------------------------------------------------------------
    void Start ()
    {
        Alfa = 0.0f;
        FadeSpeed = 0.01f;
    }

    // Update is called once per frame
    void Update ()
    {
        //フラグがtrueなら遷移を開始
        if (timeCount.TimeUpFlag == true)
        {
            //α値を加算してImageの透明度を変化
            Alfa += FadeSpeed;
        }

        if(Alfa>=1.0f)
        {
            SceneManager.LoadScene("Result");
        }

        //フェード用のImageのα値を渡す
        FadeImage.color = new Color(0.0f, 0.0f, 0.0f, Alfa);
    }

}
