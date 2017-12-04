using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//===================================================
//タイトル画面からの遷移を制御するスクリプト
//===================================================

public class TitleSceneChange : MonoBehaviour
{
    //チュートリアル画面かマッチング画面へ遷移するかのステートマシン
    enum TRANSITION_STATE
    {
        MATCHINGROOM,   //マッチングルーム
        TUTORIAL        //チュートリアル画面
    }
    private TRANSITION_STATE transitionState;

    public GameObject Arrow;
    private Vector3 pos;

    //効果音再生用変数
    private AudioSource sound01;
    private AudioSource sound02;

    //フェードアウト処理用変数
    //実際に画面を暗くするImage格納用変数
    public Image FadeImage;
    //Imageのα値
    private float Alfa;
    //フェードアウトのスピード
    private float FadeSpeed;
    //フェードアウトを実行するかのフラグ
    private bool FadeFlag;

    // 初期化---------------------------------------------------------------------------------------------
    void Start()
    {
        transitionState = TRANSITION_STATE.MATCHINGROOM;
        pos = Arrow.transform.position;

        AudioSource[] audioSources = GetComponents<AudioSource>();
        sound01 = audioSources[0];
        sound02 = audioSources[1];

        Alfa = 0.0f;
        FadeSpeed = 0.01f;
        FadeFlag = false;
    }

    // Update is called once per frame
    void Update ()
    {
        switch (transitionState)
        {
            case TRANSITION_STATE.MATCHINGROOM://マッチングルームへの遷移

                pos.y = 1.25f;

                //矢印キーの上下どちらかを押した場合に処理
                if (Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.DownArrow))
                {
                    //フェードアウトが実行されていないなら処理される
                    if (FadeFlag == false)
                    {
                        //ステートをTUTORIALに変更
                        transitionState = TRANSITION_STATE.TUTORIAL;
                        sound01.PlayOneShot(sound01.clip);
                    }
                }

                //α値が1.0fを超えたら画面が真っ暗になるので画面遷移を開始
                if (Alfa >= 1.0f)
                {
                    //マッチングルームへ遷移する
                    SceneManager.LoadScene("MatchingRoom");
                }

                break;
            case TRANSITION_STATE.TUTORIAL://チュートリアル画面への遷移

                pos.y = -1.35f;

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (FadeFlag == false)
                    {
                        //ステートをMATCHINGROOMに変更
                        transitionState = TRANSITION_STATE.MATCHINGROOM;
                        sound01.PlayOneShot(sound01.clip);
                    }
                }

                //α値が1.0fを超えたら画面が真っ暗になるので画面遷移を開始
                if (Alfa >= 1.0f)
                {
                    //チュートリアル画面へ遷移する
                    SceneManager.LoadScene("Tutorial");
                }

                break;
        }

        //エンターキーを押したら処理
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //フェードアウトを実行していない場合にのみ処理
            if (FadeFlag == false)
            {
                //効果音を再生
                sound02.PlayOneShot(sound02.clip);
                //フラグをtrueにしてフェードアウトを実行
                FadeFlag = true;
            }
        }

        //フラグがtrueなら処理
        if(FadeFlag==true)
        {
            //α値を加算してフェードアウト
            Alfa += FadeSpeed;
        }
        //実際のImageの色
        FadeImage.color = new Color(0.0f, 0.0f, 0.0f, Alfa);


        Arrow.transform.position = pos;
    }
}
