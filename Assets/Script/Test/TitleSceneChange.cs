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
        START,
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

    //タイトルでの指示を表示するテキストのImage
    //初めに○ボタンを押すように示すテキスト
    public Image ButtonPushText;
    //マッチングルームへ遷移すると示すテキスト
    public Image MatchingGoText;
    //説明画面へ遷移すると示すテキスト
    public Image TutorialGoText;

    //○ボタンが押されているかのフラグ
    private bool CircleButtonPushFlag;
    //アナログスティックの入力があるかのフラグ
    private bool SelectFlag;

    // 初期化---------------------------------------------------------------------------------------------
    void Start()
    {
        transitionState = TRANSITION_STATE.START;
        pos = Arrow.transform.position;

        AudioSource[] audioSources = GetComponents<AudioSource>();
        sound01 = audioSources[0];
        sound02 = audioSources[1];

        Alfa = 0.0f;
        FadeSpeed = 0.01f;
        FadeFlag = false;

        CircleButtonPushFlag = false;
        SelectFlag = false;
    }

    // Update is called once per frame
    void Update ()
    {
        switch (transitionState)
        {
            case TRANSITION_STATE.START://最初の状態

                //○ボタンを押したら処理
                if(Input.GetButton("Circle"))
                {
                    //○ボタンを押すように示すテキストを非表示に
                    ButtonPushText.gameObject.SetActive(false);
                    //他のテキストを表示するように
                    MatchingGoText.gameObject.SetActive(true);
                    TutorialGoText.gameObject.SetActive(true);
                    //どの選択肢を選んでいるか分かるようにするオブジェクト
                    Arrow.SetActive(true);
                    //マッチングルームへ遷移可能な状態に
                    transitionState = TRANSITION_STATE.MATCHINGROOM;
                    //効果音を再生
                    sound02.PlayOneShot(sound02.clip);
                    //ボタンは押されているのでフラグをtrueに
                    CircleButtonPushFlag = true;
                }

                break;
            case TRANSITION_STATE.MATCHINGROOM://マッチングルームへの遷移


                pos.y = -0.12f;

                //矢印キーの上下どちらかを押した場合に処理
                if (Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
                {
                    //フェードアウトが実行されていないなら処理される
                    if (FadeFlag == false && SelectFlag == false)
                    {
                        //ステートをTUTORIALに変更
                        transitionState = TRANSITION_STATE.TUTORIAL;
                        //効果音の再生
                        sound01.PlayOneShot(sound01.clip);
                        //アナログスティックの入力が行われたのでtrueに
                        SelectFlag = true;
                    }
                }
                else
                {
                    //入力が無いのでフラグはfalseに
                    SelectFlag = false;
                }


                if (Input.GetButton("Circle"))
                {
                    if (FadeFlag == false && CircleButtonPushFlag == false)
                    {
                        //効果音を再生
                        sound02.PlayOneShot(sound02.clip);
                        //フラグをtrueにしてフェードアウトを実行
                        FadeFlag = true;
                    }
                }
                else
                {
                    CircleButtonPushFlag = false;
                }

                //α値が1.0fを超えたら画面が真っ暗になるので画面遷移を開始
                if (Alfa >= 1.0f)
                {
                    //マッチングルームへ遷移する
                    SceneManager.LoadScene("MatchingRoom");
                }

                break;
            case TRANSITION_STATE.TUTORIAL://チュートリアル画面への遷移

                pos.y = -2.47f;

                if (Input.GetAxisRaw("Vertical") > 0.1 || Input.GetAxisRaw("Vertical") < -0.1)
                {
                    if (FadeFlag == false && SelectFlag == false)
                    {
                        //ステートをMATCHINGROOMに変更
                        transitionState = TRANSITION_STATE.MATCHINGROOM;
                        sound01.PlayOneShot(sound01.clip);
                        SelectFlag = true;
                    }
                }
                else
                {
                    SelectFlag = false;
                }

                if (Input.GetButton("Circle"))
                {
                    //フェードアウトを実行していない場合にのみ処理
                    if (FadeFlag == false && CircleButtonPushFlag == false)
                    {
                        //効果音を再生
                        sound02.PlayOneShot(sound02.clip);
                        //フラグをtrueにしてフェードアウトを実行
                        FadeFlag = true;
                    }
                }
                else
                {
                    CircleButtonPushFlag = false;
                }


                //α値が1.0fを超えたら画面が真っ暗になるので画面遷移を開始
                if (Alfa >= 1.0f)
                {
                    //チュートリアル画面へ遷移する
                    SceneManager.LoadScene("Tutorial");
                }

                break;
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
