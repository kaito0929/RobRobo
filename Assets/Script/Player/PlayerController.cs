﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//============================================
//キャラの制御を行うスクリプト
//============================================

public class PlayerController : MonoBehaviour
{
    // 変数宣言---------------------------------------------------------------------------------------------
    //アニメーション再生速度設定
    private float AnimSpeed;

    //キャラクターコントローラ用パラメーター
    //前進速度
    private float ForwardSpeed;
    //後退速度
    private float BackwardSpeed;
    //旋回速度
    private float RotateSpeed;

    //キャラクターコントローラ（カプセルコライダ）の移動量
    private Vector3 velocity;
    //キャラにアタッチされるアニメーターへの参照
    private Animator anim;

    //PunchHitスクリプト参照用変数
    private PunchHit punchHit;

    private Vector3[] pos = new Vector3[4];

    //キャラにアタッチされるPhotonViewへの参照
    private PhotonView photonView = null;
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    // 初期化---------------------------------------------------------------------------------------------
    void Start()
    {
        AnimSpeed = 1.5f;
        ForwardSpeed = 7.0f;
        BackwardSpeed = 2.0f;
        RotateSpeed = 2.0f;
        //Animatorコンポーネントを取得する
        anim = GetComponent<Animator>();

        punchHit = GetComponent<PunchHit>();

        pos[0] = new Vector3(-155.0f, 0.0f, 195.0f);
        pos[1] = new Vector3(-155.0f, 0.0f, -195.0f);
        pos[2] = new Vector3(155.0f, 0.0f, 195.0f);
        pos[3] = new Vector3(155.0f, 0.0f, -195.0f);

    }


    void FixedUpdate()
    {
        //メインゲーム内かマッチングルーム内でしか行動が処理されるようにする
        //リザルト画面で行動が出来ないようにするため
        if (SceneManager.GetActiveScene().name == "Main" || SceneManager.GetActiveScene().name == "MatchingRoom")
        {
            //持ち主でないのなら制御させない
            if (!photonView.isMine)
            {
                return;
            }

            //入力デバイスの水平軸をhで定義
            float h = 0.0f;
            //入力デバイスの垂直軸をvで定義
            float v = 0.0f;

            //相手からの攻撃（ロケットパンチ）が当たっていない場合に処理
            //移動や回転が処理される
            if (punchHit.PunchHitFlag == false)
            {
                if (Input.GetAxisRaw("Horizontal") > 0.1 || Input.GetAxisRaw("Horizontal2") > 0.1)
                {
                    h += 1f;
                }
                if (Input.GetAxisRaw("Horizontal") < -0.1 || Input.GetAxisRaw("Horizontal2") < -0.1)
                {
                    h -= 1f;
                }

                if (Input.GetAxisRaw("Vertical") > 0.1)
                {
                    v += 1f;
                }
                if (Input.GetAxisRaw("Vertical") < -0.1)
                {
                    v -= 1f;
                }
            }

            //Animator側で設定している"Speed"パラメーターにvを渡す
            anim.SetFloat("Speed", v);
            //Animator側で設定している"Direction"パラメーターにhを渡す
            anim.SetFloat("Direction", h);
            //Animatorのモーション再生速度にAnimSpeedを設定する
            anim.speed = AnimSpeed;


            //キャラクターの移動処理
            //上下のキー入力Z軸方向の移動量を取得
            velocity = new Vector3(0, 0, v);
            //キャラクターのローカル空間での方向に変換
            velocity = transform.TransformDirection(velocity);
            //以下のvの閾値は、Mecanim側のトランジションと一緒に調整する
            if (v > 0.1)
            {
                //移動速度を掛ける
                velocity *= ForwardSpeed;
            }
            else if (v < -0.1)
            {
                //移動速度を掛ける
                velocity *= BackwardSpeed;
            }

            //上下のキー入力でキャラクターを移動させる
            transform.localPosition += velocity * Time.fixedDeltaTime;

            //左右のキー入力でキャラクタをY軸で旋回させる
            transform.Rotate(0, h * RotateSpeed, 0);
        }
        else
        {

            //持ち主でないのなら制御させない
            if (!photonView.isMine)
            {
                return;
            }

            switch (gameObject.tag)
            {
                case "Player1":
                    gameObject.transform.position = pos[0];
                    break;
                case "Player2":
                    gameObject.transform.position = pos[1];
                    break;
                case "Player3":
                    gameObject.transform.position = pos[2];
                    break;
                case "Player4":
                    gameObject.transform.position = pos[3];
                    break;
            }
            transform.rotation = Quaternion.Euler(0, 180, 0);

        }
    }


}
