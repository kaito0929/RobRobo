using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//===============================================
//ロケットパンチにアタッチする
//パンチが当たった際に処理を行うスクリプト
//===============================================

public class RocketpunchColl : MonoBehaviour
{
    // 変数宣言---------------------------------------------------------------------------------------------

    //ロケットパンチがステージか何かに衝突したかのフラグ
    public bool PunchCollFlag;

    //ロケットパンチが装備に当たったかのフラグ
    //頭装備
    public bool HeadPartsPunchCollFlag;
    //体装備
    public bool BodyPartsPunchCollFlag;
    //腕装備
    public bool ArmPartsPunchCollFlag;
    //足装備
    public bool LegPartsPunchCollFlag;


    //衝突した装備のランクを取得するための変数
    //頭装備
    public int HeadRankNum;
    //体装備
    public int BodyRankNum;
    //腕装備
    public int ArmRankNum;
    //足装備
    public int LegRankNum;

    //自分の操作するキャラクターのオブジェクト
    //当たり判定が発生しないために宣言
    public GameObject myCharacter;


    //自分が装着している装備かどうかを判別するための変数
    //頭装備
    public GameObject HeadPartsObj;
    //体装備
    public GameObject BodyPartsObj;
    //腕装備
    public GameObject[] ArmPartsObj = new GameObject[6];
    //足装備
    public GameObject[] LegPartsObj = new GameObject[4];



    // 初期化----------------------------------------------------------------------------------------------
    void Start()
    {
        PunchCollFlag = false;
        HeadPartsPunchCollFlag = false;
        BodyPartsPunchCollFlag = false;
        ArmPartsPunchCollFlag = false;
        LegPartsPunchCollFlag = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 衝突処理---------------------------------------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        //自分の操作するキャラ意外と衝突したら処理
        if (other.gameObject != myCharacter)
        {
            //何かと衝突した場合にフラグをtrueに
            //ステージなどとぶつかった場合のため
            PunchCollFlag = true;


            //装備とロケットパンチが衝突した場合の処理
            //頭装備----------------------------------------------------------------------------------
            if (other.gameObject.tag=="Head")
            {
                //衝突した装備が自分が装着しているもの以外の場合に処理
                if (other.gameObject != HeadPartsObj)
                {
                    //それぞれの部位にぶつかったフラグをtrueにする
                    HeadPartsPunchCollFlag = true;
                    //衝突した装備のItemRankスクリプト内の変数を代入
                    HeadRankNum = other.GetComponent<HeadPartsMaterialChange>().MaterialNumber;
                }
            }
            //他の部位の装備の処理が以下に描かれているが
            //基本的に内容は上記と同じなのでコメントは省略
            //コメントを参照する場合は上記のコメントを参照してください

            // 体装備----------------------------------------------------------------------------------
            if (other.gameObject.tag == "Body")
            {
                BodyPartsPunchCollFlag = true;
                BodyRankNum = other.GetComponent<BodyPartsMaterialChange>().MaterialNumber;
            }

            // 腕装備----------------------------------------------------------------------------------
            if (other.gameObject.tag == "Arm")
            {
                if (other.gameObject != ArmPartsObj[0] &&
                    other.gameObject != ArmPartsObj[1] &&
                    other.gameObject != ArmPartsObj[2] &&
                    other.gameObject != ArmPartsObj[3] &&
                    other.gameObject != ArmPartsObj[4] &&
                    other.gameObject != ArmPartsObj[5])
                {
                    ArmPartsPunchCollFlag = true;
                    ArmRankNum = other.GetComponent<ArmPartsMaterialChange>().MaterialNumber;
                }
            }

            // 足装備----------------------------------------------------------------------------------
            if (other.gameObject.tag == "Leg")
            {
                LegPartsPunchCollFlag = true;
                LegRankNum = other.GetComponent<LegPartsMaterialChange>().MaterialNumber;
            }
        }
    }


}
