using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=======================================
//ステージ上の装備を取得した時の処理
//=======================================

public class StagePartsGet : MonoBehaviour
{
    // 変数宣言---------------------------------------------------------------------------------------------

    //ステージ上のパーツにプレイヤーやロケットパンチが
    //衝突したかのフラグ。衝突したらtrueになる
    private bool CollFlag;

    //キャラにアタッチされるPhotonViewへの参照
    private PhotonView photonView = null;
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }


    // 初期化---------------------------------------------------------------------------------------------
    void Start()
    {
        CollFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CollFlag == true)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1"|| other.gameObject.tag == "Player2" ||
            other.gameObject.tag == "Player3"|| other.gameObject.tag == "Player4" ||
            other.gameObject.tag == "Punch")
        {
            photonView.RPC("PartsHide", PhotonTargets.All);
        }
    }

    [PunRPC]
    void PartsHide()
    {
        CollFlag = true;
    }

}

