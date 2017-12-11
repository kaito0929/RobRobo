﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//===============================================
//腕装備が拳と衝突した場合の処理を行うスクリプト
//===============================================

public class ArmColl : MonoBehaviour
{
    // 変数宣言---------------------------------------------------------------------------------------------

    //PlayerItemGetスクリプトの参照
    public PlayerItemGet playerItemGet;

    //キャラにアタッチされるPhotonViewへの参照
    private PhotonView photonView = null;
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    void OnTriggerEnter(Collider other)
    {
        //isMainで自分自身の操作しか受け付けないようにしておく
        //これをやっておかないと、相手に一方的に処理される可能性がある
        if (photonView.isMine)
        {
            //ロケットパンチが当たった時の処理
            if (other.gameObject.tag == "Punch")
            {
                //フラグをfalseにして装備が外れたように見せる
                playerItemGet.ArmPartsGetFlag = false;
            }
        }
    }

}
