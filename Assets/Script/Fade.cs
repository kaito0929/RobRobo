using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//======================================================
//マッチングルームで部屋に入れた際にフラグを受け取って
//フェードインを開始するスクリプト
//文字の方も一緒にフェードインする
//======================================================

public class Fade : MonoBehaviour
{
    // 変数宣言--------------------------------------------------------------------------------------------

    //フェードインを行う黒色のImage
    public Image FadeImage;
    //最後に表示されるテキスト
    public Image FadeText;

    //α値
    private float Alfa;
    //フェードインのスピード
    private float FadeSpeed;

    public PhotonManager photonManager;

    //GameStartへの参照
    //アタッチされているオブジェクトは別なのでInspectorで設定するように
    public GameStart gameStart;


    // 初期化----------------------------------------------------------------------------------------------
    void Start ()
    {
        Alfa = 1.0f;
        FadeSpeed = 0.01f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameStart.GetPlayerNecessaryNum() > 0)
        {
            //PhotonManagerスクリプト内のRoomInFlag（部屋に入れたかのフラグ）が
            //trueになった場合に処理を開始
            if (photonManager.RoomInFlag == true)
            {
                Alfa -= FadeSpeed;
            }
        }
        else
        {
            Alfa += FadeSpeed;
        }

        if (Alfa >= 1.5f)
        {
            SceneManager.LoadScene("Main");
        }

        FadeImage.color = new Color(0.0f, 0.0f, 0.0f, Alfa);
        FadeText.color = new Color(255.0f, 255.0f, 255.0f, Alfa);
    }
}
