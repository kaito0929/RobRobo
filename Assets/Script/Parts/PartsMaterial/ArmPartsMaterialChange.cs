using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//============================================
//腕装備のマテリアルを切り替えるスクリプト
//============================================

public class ArmPartsMaterialChange : MonoBehaviour
{

    // 変数宣言---------------------------------------------------------------------------------------------

    //装備品のマテリアル
    //配列変数で宣言してそれぞれのランクの色のマテリアルをアタッチ
    //数値を切り替えることで装備の見た目を変えることが出来る
    public Material[] PartsMaterial = new Material[3];

    //マテリアルを切り替える数値
    public int MaterialNumber;

    //腕装備に振り分けられたポイント
    public int ArmPartsPoint;    

    //キャラにアタッチされるPhotonViewへの参照
    private PhotonView photonView = null;
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    // 初期化---------------------------------------------------------------------------------------------
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            ////装備の状態に応じてスコアを変更
            switch (MaterialNumber)
            {
                case 0:
                    ArmPartsPoint = 1;
                    break;
                case 1:
                    ArmPartsPoint = 2;
                    break;
                case 2:
                    ArmPartsPoint = 3;
                    break;
            }
        }

        GetComponent<Renderer>().material = PartsMaterial[MaterialNumber];

    }


    //変数の同期
    void OnPhotonSerializeView(PhotonStream i_stream, PhotonMessageInfo i_info)
    {
        if (i_stream.isWriting)
        {
            //データの送信
            i_stream.SendNext(this.MaterialNumber);
            i_stream.SendNext(this.ArmPartsPoint);
        }
        else
        {
            //データの受信
            this.MaterialNumber = (int)i_stream.ReceiveNext();
            this.ArmPartsPoint = (int)i_stream.ReceiveNext();
        }
    }
}
