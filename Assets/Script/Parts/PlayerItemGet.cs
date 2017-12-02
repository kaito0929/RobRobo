using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//=====================================================
//プレイヤーがアイテムを手に入れる処理を行うスクリプト
//気を付けるのは相手プレイヤーの変数を操作するのは
//OnPhotonSerializeViewだと無理そうなので
//攻撃を受けて自身の変数を操作する処理にするのがよさそう
//=====================================================

public class PlayerItemGet : MonoBehaviour
{

    //装備をゲットしたか判別するフラグ
    //頭装備
    public bool HeadPartsGetFlag;
    //体装備
    public bool BodyPartsGetFlag;
    //腕装備
    public bool ArmPartsGetFlag;
    //足装備
    public bool LegPartsGetFlag;

    //自分が装着している装備かどうかを判別するための変数
    //頭装備
    public GameObject HeadPartsObj;
    //体装備
    public GameObject BodyPartsObj;
    //腕装備
    public GameObject[] ArmPartsObj = new GameObject[6];
    //足装備
    public GameObject[] LegPartsObj = new GameObject[4];

    //自分が装着している装備のマテリアルを変更するために宣言
    //当たったアイテムのItemRankスクリプト内の数値を代入することでマテリアルを切り替える
    //頭装備
    public HeadPartsMaterialChange headPartsMaterialChange;
    //体装備
    public BodyPartsMaterialChange bodyPartsMaterialChange;
    //腕装備
    public ArmPartsMaterialChange[] armPartsMaterialChange = new ArmPartsMaterialChange[6];
    //足装備
    public LegPartsMaterialChange[] legPartsMaterialChange = new LegPartsMaterialChange[4];

    public int num;

    //キャラにアタッチされるPhotonViewへの参照
    private PhotonView photonView = null;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Use this for initialization
    void Start()
    {
        HeadPartsGetFlag = false;
        BodyPartsGetFlag = false;
        ArmPartsGetFlag = false;
        LegPartsGetFlag = false;
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

            //落ちている装備アイテムを拾った際に行う処理
            //頭装備----------------------------------------------------------------------------------
            if (other.gameObject.tag == "Head")
            {
                //衝突したオブジェクトが自分が装着したオブジェクトではなかった場合に処理
                if (other.gameObject != HeadPartsObj)
                {
                    //装備を手に入れたので、自分の装着している装備を表示
                    HeadPartsGetFlag = true;
                    //当たったオブジェクトの表示を消す
                    other.gameObject.SetActive(false);
                    //自分の装着する装備のマテリアルを衝突したオブジェクトの
                    //マテリアルと同じにする
                    headPartsMaterialChange.MaterialNumber = other.gameObject.GetComponent<ItemRank>().MaterialNumber;
                }
            }
            //以下の処理も上記の頭装備と基本的に変わらないので
            //コメントについてはそちらの方を参照してください

            //体装備----------------------------------------------------------------------------------
            if (other.gameObject.tag == "Body")
            {
                if (other.gameObject != BodyPartsObj)
                {
                    BodyPartsGetFlag = true;
                    other.gameObject.SetActive(false);
                    bodyPartsMaterialChange.MaterialNumber = other.gameObject.GetComponent<ItemRank>().MaterialNumber;
                }
            }

            //腕装備----------------------------------------------------------------------------------
            if (other.gameObject.tag == "Arm")
            {
                if (other.gameObject != ArmPartsObj[0] &&
                    other.gameObject != ArmPartsObj[1] &&
                    other.gameObject != ArmPartsObj[2] &&
                    other.gameObject != ArmPartsObj[3] &&
                    other.gameObject != ArmPartsObj[4] &&
                    other.gameObject != ArmPartsObj[5])
                {
                    ArmPartsGetFlag = true;
                    other.gameObject.SetActive(false);
                    for (int i = 0; i < 6; i++)
                    {
                        armPartsMaterialChange[i].MaterialNumber = other.gameObject.GetComponent<ItemRank>().MaterialNumber;
                    }
                }
            }

            // 足装備----------------------------------------------------------------------------------
            if (other.gameObject.tag == "Leg")
            {
                if (other.gameObject != LegPartsObj[0] &&
                    other.gameObject != LegPartsObj[1] &&
                    other.gameObject != LegPartsObj[2] &&
                    other.gameObject != LegPartsObj[3])
                {
                    LegPartsGetFlag = true;
                    other.gameObject.SetActive(false);
                    for (int i = 0; i < 2; i++)
                    {
                        legPartsMaterialChange[i].MaterialNumber = other.gameObject.GetComponent<ItemRank>().MaterialNumber;
                    }
                }
            }


        }
    }



    //変数の同期
    void OnPhotonSerializeView(PhotonStream i_stream, PhotonMessageInfo i_info)
    {
        if (i_stream.isWriting)
        {
            //データの送信
            i_stream.SendNext(this.HeadPartsGetFlag);
            i_stream.SendNext(this.BodyPartsGetFlag);
            i_stream.SendNext(this.ArmPartsGetFlag);
            i_stream.SendNext(this.LegPartsGetFlag);
        }
        else
        {
            //データの受信
            this.HeadPartsGetFlag = (bool)i_stream.ReceiveNext();
            this.BodyPartsGetFlag = (bool)i_stream.ReceiveNext();
            this.ArmPartsGetFlag = (bool)i_stream.ReceiveNext();
            this.LegPartsGetFlag = (bool)i_stream.ReceiveNext();
        }
    }

}
