﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//=================================================
//プレイヤーを区別するためにタグをつけるスクリプト
//=================================================

public class PlayerNumberTag : MonoBehaviour
{
    //プレイヤーID取得用の変数
    private static int PlayerWhoIsIt;

    //キャラにアタッチされるPhotonViewへの参照
    private PhotonView photonView = null;
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Use this for initialization
    void Start()
    {
        //プレイヤーIDを取得
        PlayerWhoIsIt = PhotonNetwork.player.ID;

        //自分以外の操作を受け付けない
        //この処理を行わないとタグがそれぞれに付けることが出来ない
        if (photonView.isMine)
        {
            //プレイヤーIDによって付けるタグを切り替える
            //処理を行うのはPunRPCで宣言した関数内で処理する
            //タグは同期しておかないといけない
            if (PlayerWhoIsIt == 1)
            {
                //プレイヤーIDが1なのでPlayer1のタグをつける
                //以下の処理も数値が違うだけで処理は同じなのでコメントは省略
                photonView.RPC("GetTag1", PhotonTargets.All);
            }
            if (PlayerWhoIsIt == 2)
            {
                photonView.RPC("GetTag2", PhotonTargets.All);
            }
            if (PlayerWhoIsIt == 3)
            {
                photonView.RPC("GetTag3", PhotonTargets.All);
            }
            if (PlayerWhoIsIt == 4)
            {
                photonView.RPC("GetTag4", PhotonTargets.All);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }


    //タグを付ける関数
    //PunRPCで宣言して同期を行わないといけない
    [PunRPC]
    private void GetTag1()
    {
        gameObject.tag = "Player1";
    }

    [PunRPC]
    private void GetTag2()
    {
        gameObject.tag = "Player2";
    }

    [PunRPC]
    private void GetTag3()
    {
        gameObject.tag = "Player3";
    }

    [PunRPC]
    private void GetTag4()
    {
        gameObject.tag = "Player4";
    }


}
