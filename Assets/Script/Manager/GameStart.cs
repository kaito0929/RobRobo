using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//==============================================================
//部屋に何人入ったか取得するスクリプト
//必要人数の変数があるのでこのスクリプトは必ず
//PhotonViewで同期を取ること
//同期を取らないと部屋を立てた人だけが画面遷移をしてしまう
//==============================================================

public class GameStart : MonoBehaviour
{
    // 変数宣言---------------------------------------------------------------------------------------------
    //ゲーム開始の為に必要なプレイヤーの数
    //部屋が出来ているということは既に一人いるので残り三人
    private int PlayerNecessaryNum;
    public int GetPlayerNecessaryNum()
    {
        return PlayerNecessaryNum;
    }

    // Use this for initialization
    void Start ()
    {
        //DontDestroyOnLoadでこのスクリプトをアタッチしているオブジェクトを
        //残しておかないと画面遷移が不安定になる
        //PlayerNecessaryNumの送信が削除の前に間に合うと画面遷移できるのかもしれない
        DontDestroyOnLoad(gameObject);
        PhotonNetwork.ConnectUsingSettings(Application.version);

        PlayerNecessaryNum = 1;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            PlayerNecessaryNum = 1;
        }
    }
 
    //部屋にプレイヤーが入ってくると呼ばれるコールバック
    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        //部屋にプレイヤーが入ってきたので
        //数値を減らして必要数をカウント
        PlayerNecessaryNum--;
    }

    //部屋からプレイヤーが出ていったら呼ばれるコールバック
    void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        //部屋からプレイヤーが出て行ったので
        //数値を増やして残り必要数をカウント
        PlayerNecessaryNum++;
    }

    //プレイヤーの数を同期
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //データの送信
            stream.SendNext(PlayerNecessaryNum);
        }
        else
        {
            //データの受信
            this.PlayerNecessaryNum = (int)stream.ReceiveNext();
        }
    }

}
