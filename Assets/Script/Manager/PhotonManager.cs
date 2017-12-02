using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManager : Photon.MonoBehaviour
{
    //Photonの接続状況を表示するテキストの画像
    //public Image[] StateTextImage = new Image[3];

    //Photonに接続するコード
    void Start()
    {
        //ConnectUsingSettingsの引数に指定している
        //v1.0という文字列は別に何でも構わないらしい
        //もしリリース後のアップデートで、アプリを最新化していないプレイヤーと、
        //アプリを最新化済みのユーザー同士で対戦させたくない場合、最新側で例えば(v1.1)等として、
        //文字列を変えれば、ロビー空間を別にすることが出来、バージョンの違いによる不具合を
        //出さないようにすることが出来るらしい
        PhotonNetwork.ConnectUsingSettings("v1.0");

        //for (int i = 0; i < 3; i++)
        //{
        //    StateTextImage[i].gameObject.SetActive(false);
        //}
    }
    //PhotonServerSettingsのAuto-Join Lobbyにチェックを入れていると
    //自動的にロビーに入るようになっている

    //Photonへの接続が完了
    //OnJoinedLobbyが呼ばれる
    void OnJoinedLobby()
    {
        //JoinRandomRoomで入室
        //既存のルームにランダムで入室
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("PhotonManager OnJoinedLobby");
        //StateTextImage[0].gameObject.SetActive(true);
    }

    //入室失敗時に呼ばれるコールバック
    void OnPhotonRandomJoinFailed()
    {
        //入室が失敗ということはルームが無いということになるので
        //CreateRoomでルームを作成
        PhotonNetwork.CreateRoom("RoomName", new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 }, null);
        Debug.Log("PhotonManager CreateRoom");
        //StateTextImage[0].gameObject.SetActive(false);
        //StateTextImage[1].gameObject.SetActive(true);
    }

    //ルーム入室した時に呼ばれるコールバックメソッド
    void OnJoinedRoom()
    {
        Debug.Log("PhotonManager OnJoinedRoom");
        //StateTextImage[0].gameObject.SetActive(false);
        //StateTextImage[1].gameObject.SetActive(false);
        //StateTextImage[2].gameObject.SetActive(true);
    }

}
