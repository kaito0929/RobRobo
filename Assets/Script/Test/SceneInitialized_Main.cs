using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SceneInitialized_Main : MonoBehaviour
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

        if (isAllPlayerLoaded)
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

    // Use this for initialization
    void Start()
    {
        //シーン遷移完了のフラグをセット
        Ready();
        //最後にシーン遷移した人のみ
        //OnPhotonPlayerPropertiesChangedが
        //実行されない場合を考慮して自分で一回実行する
        CheckAllPlayerState();
    }

    //初期化処理
    void Initialize()
    {

    }

}
