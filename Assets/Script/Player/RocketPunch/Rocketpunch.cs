using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==============================================
//ロケットパンチの挙動を処理するスクリプト
//==============================================

public class Rocketpunch : MonoBehaviour
{
    // 変数宣言---------------------------------------------------------------------------------------------

    //ロケットパンチを放っているかのフラグ
    private bool PunchFlag;

    //実際に打ち出すロケットパンチのオブジェクト
    public GameObject RocketpunchObj;
    //ロケットパンチの拳の元の位置
    //発射した後、戻る位置として設定する
    public GameObject OriginalPosObj;

    //拳と元の位置との距離
    private float distance;

    //移動スピード
    private float Speed;
    //動かす変数
    private float Step;


    //拳にアタッチされたTestRocketpunchCollへの参照
    public RocketpunchColl rocketpunchColl;
    //PlayerItemGetスクリプト参照用変数
    public PlayerItemGet itemGet;

    //カメラの位置
    private Transform cameraTransform;
    //CameraWorkスクリプト参照用変数
    public CameraWork cameraWork;


    //装備の表示されているマテリアルを取得するための変数
    //頭装備
    public HeadPartsMaterialChange headPartsMaterialChange;
    //体装備
    public BodyPartsMaterialChange bodyPartsMaterialChange;
    //腕装備
    public ArmPartsMaterialChange[] armPartsMaterialChange=new ArmPartsMaterialChange[6];
    //足装備
    public LegPartsMaterialChange[] legPartsMaterialChange=new LegPartsMaterialChange[4];

    //キャラにアタッチされるPhotonViewへの参照
    private PhotonView photonView = null;
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }


    // 初期化-----------------------------------------------------------------------------------------------
    void Start ()
    {
        PunchFlag = false;
        Speed = 6.0f;
        Step = 0.0f;

        //メインカメラの座標を取得
        cameraTransform = Camera.main.transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //自分以外の操作を受け付けないように
        if (photonView.isMine)
        {
            PunchShoot();
            PunchReturn();
            PunchMove();

            //拳の位置が発射位置から10.0f離れていたら処理
            if (distance >= 10.0f)
            {
                //発射フラグをfalseにして移動を終了
                PunchFlag = false;
            }

            //拳がプレイヤーかステージなどにぶつかった場合に処理
            if (rocketpunchColl.PunchCollFlag == true)
            {
                //PunchFlagをfalseにして拳の進行を終了
                //フラグがfalseになったので元の位置へ戻るようになる
                PunchFlag = false;
            }

        }
    }

    //ロケットパンチの発射を処理する関数
    void PunchShoot()
    {
        //カメラの状態がAIMだとロケットパンチが発射できるようにする
        if (cameraWork.cameraState == CameraWork.CAMERA_STATE.AIM)
        {
            //パンチの発射
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //パンチの角度をカメラの角度と同じにする
                RocketpunchObj.transform.rotation = cameraTransform.rotation;
                //フラグを操作
                PunchFlag = true;
            }
        }
    }

    //ロケットパンチが戻ってきた時の処理を行う関数
    void PunchReturn()
    {
        //ロケットパンチが自分の元に帰ってきた時
        if (distance <= 0.1f)
        {
            //パンチが何かに衝突したフラグをfalseに
            if (rocketpunchColl.PunchCollFlag == true)
            {
                //フラグをfalseにする
                rocketpunchColl.PunchCollFlag = false;
            }

            //パンチが装備にぶつかっていた場合に処理
            //頭装備----------------------------------------------------------------
            if (rocketpunchColl.HeadPartsPunchCollFlag == true)
            {
                //装備を手に入れたフラグをtrueにする
                itemGet.HeadPartsGetFlag = true;
                //戻ってきたのでパンチのゲットフラグをfalseに
                rocketpunchColl.HeadPartsPunchCollFlag = false;

                if (headPartsMaterialChange.MaterialNumber <= rocketpunchColl.HeadRankNum)
                {
                    //ロケットパンチで奪うことの出来た装備のランクを反映
                    headPartsMaterialChange.MaterialNumber = rocketpunchColl.HeadRankNum;
                }
            }

            //他の装備の処理も基本的に同じなのでコメントは省略
            //上記を参照してください
            //体装備----------------------------------------------------------------
            if (rocketpunchColl.BodyPartsPunchCollFlag == true)
            {
                itemGet.BodyPartsGetFlag = true;
                rocketpunchColl.BodyPartsPunchCollFlag = false;

                if (bodyPartsMaterialChange.MaterialNumber <= rocketpunchColl.BodyRankNum)
                {
                    bodyPartsMaterialChange.MaterialNumber = rocketpunchColl.BodyRankNum;
                }
            }

            //腕装備----------------------------------------------------------------
            if (rocketpunchColl.ArmPartsPunchCollFlag == true)
            {
                itemGet.ArmPartsGetFlag = true;
                rocketpunchColl.ArmPartsPunchCollFlag = false;

                if (armPartsMaterialChange[0].MaterialNumber <= rocketpunchColl.ArmRankNum)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        armPartsMaterialChange[i].MaterialNumber = rocketpunchColl.ArmRankNum;
                    }
                }
            }

            //足装備----------------------------------------------------------------
            if (rocketpunchColl.LegPartsPunchCollFlag == true)
            {
                itemGet.LegPartsGetFlag = true;
                rocketpunchColl.LegPartsPunchCollFlag = false;

                if (legPartsMaterialChange[0].MaterialNumber <= rocketpunchColl.LegRankNum)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        legPartsMaterialChange[i].MaterialNumber = rocketpunchColl.LegRankNum;
                    }
                }
            }
        }

    }

    //パンチの移動を処理する関数
    void PunchMove()
    {
        //パンチを発射している場合に処理
        if (PunchFlag == true)
        {
            //パンチを前方に向かって発射
            RocketpunchObj.transform.position += RocketpunchObj.transform.forward;
        }

        //拳と発射位置の距離を測る
        Vector3 Apos = RocketpunchObj.transform.position;
        Vector3 Bpos = OriginalPosObj.transform.position;
        distance = Vector3.Distance(Apos, Bpos);

        //発射位置から離れていて、フラグがfalseなら処理を開始
        //拳を元の位置へ戻す
        if (distance >= 0.0f && PunchFlag == false)
        {
            //動くスピード
            Step = Time.deltaTime * Speed;
            RocketpunchObj.transform.position = Vector3.MoveTowards
                (RocketpunchObj.transform.position, OriginalPosObj.transform.position, Step);
        }
    }


}
