﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//===============================================
//足装備が拳と衝突した場合の処理を行うスクリプト
//===============================================

public class LegColl : MonoBehaviour
{
    // 変数宣言---------------------------------------------------------------------------------------------

    //自キャラのロケットパンチ
    public GameObject PunchObj;
    //PlayerItemGetスクリプトの参照
    public PlayerItemGet playerItemGet;

    //キャラにアタッチされるPhotonViewへの参照
    private PhotonView photonView = null;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //isMainで自分自身の操作しか受け付けないようにしておく
        if (photonView.isMine)
        {
            //ロケットパンチが当たった時の処理
            if (other.gameObject.tag == "Punch")
            {
                //足の装備を着けている場合に処理される
                if (other.gameObject != PunchObj && playerItemGet.LegPartsGetFlag == true)
                {
                    //フラグをfalseにして装備が外れたように見せる
                    playerItemGet.LegPartsGetFlag = false;
                    Debug.Log("Hit");
                }
            }
        }
    }
}
