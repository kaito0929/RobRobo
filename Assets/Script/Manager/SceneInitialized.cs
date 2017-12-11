using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//===============================================
//部屋に入った四人のプレイヤーが
//シーン遷移が完了したかを検知するスクリプト
//
//参考サイト
//https://qiita.com/toRisouP/items/4fd09f7935024d7017fd
//===============================================

public class SceneInitialized : MonoBehaviour
{
    // 変数宣言---------------------------------------------------------------------------------------------
    //初期化済みか?
    private bool isInitialized = false;

    //PhotonPlayer.customPropertiesのKey
    //ゲーム中でユニークであれば何でもいいが、
    //より短い文字列の方が通信料の節約になる
    private readonly string ReadyStateKey = "SceneReady";

    //誰かのカスタムプロパティが書き終わった時に
    //通知されるPUNのコールバック
    private void OnPhotonPlayerPropertiesChanged(object[] data)
    {
        //誰かのカスタムプロパティが書き換わるたびに確認
        CheckAllPlayerState();
    }

    //全員のシーン遷移フラグが設定されているかチェックする
    private void CheckAllPlayerState()
    {
        if (isInitialized) return;

        //全員のフラグが設定されているか？
        var isAllPlayerLoaded = PhotonNetwork.playerList
                .Select(x => x.CustomProperties)
                .All(x => x.ContainsKey(ReadyStateKey) && (bool)x[ReadyStateKey]);

        if(isAllPlayerLoaded)
        {
            //全員のフラグが設定されていたら初期化開始
            isInitialized = true;
            ClearReadyStatus();
            Initialize();
        }
    }

    //準備完了したことを通知する
    private void Ready()
    {
        var cp = PhotonNetwork.player.CustomProperties;
        cp[ReadyStateKey] = true;
        PhotonNetwork.player.SetCustomProperties(cp);
    }

    //準備完了通知を削除してカスタムプロパティを綺麗にする
    private void ClearReadyStatus()
    {
        var cp = PhotonNetwork.player.CustomProperties;
        cp[ReadyStateKey] = null;
        PhotonNetwork.player.SetCustomProperties(cp);
    }

    //キャラの初期位置
    //プレイヤーのIDで位置を決定させるので
    //指定人数より一つ多く宣言しておく
    private Vector3[] pos = new Vector3[6];

    //プレイヤーID取得用の変数
    private static int PlayerWhoIsIt;

    public bool SceneChangeFlag;

    // Use this for initialization
    void Start ()
    {
        //シーン遷移完了のフラグをセット
        Ready();
        //最後にシーン遷移した人のみ
        //OnPhotonPlayerPropertiesChangedが
        //実行されない場合を考慮して自分で一回実行する
        CheckAllPlayerState();

        pos[0] = new Vector3(-24, 2, 24);
        pos[1] = new Vector3(-24, 2, 24);
        pos[2] = new Vector3(24, 2, 24);
        pos[3] = new Vector3(-24, 2, -24);
        pos[4] = new Vector3(24, 2, -24);
        pos[5] = new Vector3(24, 2, -24);

        //プレイヤーのIDを取得する
        PlayerWhoIsIt = PhotonNetwork.player.ID;
    }

    //初期化処理
    void Initialize()
    {
        SceneChangeFlag = true;
        //キャラクターを生成
        PhotonNetwork.Instantiate("robo", pos[PlayerWhoIsIt], Quaternion.identity, 0);
    }

}
