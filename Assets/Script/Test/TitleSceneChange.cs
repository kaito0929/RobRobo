using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // 初期化---------------------------------------------------------------------------------------------
    void Start()
    {
        transitionState = TRANSITION_STATE.MATCHINGROOM;
        pos = Arrow.transform.position;
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
                    //ステートをTUTORIALに変更
                    transitionState = TRANSITION_STATE.TUTORIAL;
                }

                //エンターキーを押したら処理
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //マッチングルームへ遷移する
                    SceneManager.LoadScene("MatchingRoom");
                }
                break;
            case TRANSITION_STATE.TUTORIAL://チュートリアル画面への遷移

                pos.y = -1.35f;


                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    //ステートをMATCHINGROOMに変更
                    transitionState = TRANSITION_STATE.MATCHINGROOM;
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //チュートリアル画面へ遷移する
                    SceneManager.LoadScene("Tutorial");
                }
                break;
        }
        Arrow.transform.position = pos;


    }
}
