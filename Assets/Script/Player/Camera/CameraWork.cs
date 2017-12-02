using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//================================================
//プレイヤーのカメラ操作を行うスクリプト
//キャラクターにアタッチして使用する
//
//参考サイト
//https://dskjal.com/unity/tps-camera.html
//================================================

public class CameraWork : MonoBehaviour
{
    //カメラの位置
    //カメラに対してターゲットを追従するスクリプトをアタッチする方法は使えないので
    //キャラクターにアタッチして、メインカメラの座標を取得する方法で処理するために宣言
    private Transform cameraTransform;
    //カメラとプレイヤーの距離
    public float DistanceToPlayer = 2f;
    //カメラを横にスライドさせる：プラスの時は右へ、マイナスの時は左へ
    public float SlideDistanceM = 0f;
    //注視点の高さ
    public float HeightM = 1.2f;
    //カメラの感度
    public float RotationSensitivity = 100f;

    //カメラの状態の変化
    //NORMALは通常状態のカメラの動き
    //AIMはカメラがキャラに近づいて右に少しずれる
    //そしてターゲットカーソルが表示される
    public enum CAMERA_STATE
    {
        NORMAL,
        AIM,
    }
    //CAMERA_STATE参照用の変数
    public CAMERA_STATE cameraState;

    //キャラにアタッチされるアニメーターへの参照
    private Animator anim;

    //public Image TargetCursor;

    //キャラにアタッチされるPhotonViewへの参照
    private PhotonView photonView = null;
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Use this for initialization
    void Start ()
    {
        //メインカメラのtransformを取得
        cameraTransform = Camera.main.transform;
        //Animatorコンポーネントを取得する
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (photonView.isMine)
        {

            //カメラの操作用関数
            CameraOperation();

            //カメラの状態の切り替え
            switch (cameraState)
            {
                case CAMERA_STATE.NORMAL://通常状態のカメラ

                    //カメラの位置をある程度離しておく
                    DistanceToPlayer = 3f;
                    //カメラの位置はプレイヤーを中心に据える
                    SlideDistanceM = 0.0f;

                    //照準モードではないのでターゲットカーソルは非表示にする
                    //TargetCursor.gameObject.SetActive(false);

                    anim.SetBool("Aim", false);

                    //ボタンを押すことで状態が切り替わる
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        cameraState = CAMERA_STATE.AIM;
                    }
                    break;
                case CAMERA_STATE.AIM://照準モード

                    //照準を合わせるモードなのでカメラをプレイヤーの近くに移動
                    DistanceToPlayer = 1.5f;
                    //カメラを右に少しずらす
                    SlideDistanceM = 0.3f;

                    //照準モードなのでターゲットカーソルを表示
                    //TargetCursor.gameObject.SetActive(true);

                    anim.SetBool("Aim", true);

                    //ボタンを押すことで状態が切り替わる
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        cameraState = CAMERA_STATE.NORMAL;
                    }
                    break;
            }
        }
    }


    //カメラの操作用関数
    //基本的な動きはNORMAL状態とAIM状態は一緒なので
    //関数をUpdate内で宣言しておく
    void CameraOperation()
    {
        //カメラの横回転のための変数
        var rotX = 0.0f;
        //プレイヤーと同じ方向を向かせたいので
        //操作キーをプレイヤーの方向転換のキーと同じにする
        //出来れば変えておきたい
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotX += 1f * Time.deltaTime * RotationSensitivity;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotX -= 1f * Time.deltaTime * RotationSensitivity;
        }

        //カメラの上下方向変換用の変数
        var rotY = 0.0f;
        //こっちはプレイヤーの向きとは独立させる
        if (Input.GetKey(KeyCode.W))
        {
            rotY += 1f * Time.deltaTime * RotationSensitivity;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rotY -= 1f * Time.deltaTime * RotationSensitivity;
        }

        var lookAt = gameObject.transform.position + Vector3.up * HeightM;

        //回転
        cameraTransform.RotateAround(lookAt, Vector3.up, rotX);
        //カメラがプレイヤーの真上や真下にある時にそれ以上回転させないようにする
        if (cameraTransform.forward.y > 0.9f && rotY < 0)
        {
            rotY = 0;
        }

        if (cameraTransform.forward.y < -0.9f && rotY > 0)
        {
            rotY = 0;
        }
        cameraTransform.transform.RotateAround(lookAt, cameraTransform.transform.right, rotY);

        //カメラとプレイヤーとの間の距離を調整
        cameraTransform.transform.position = lookAt - cameraTransform.transform.forward * DistanceToPlayer;
        //注視点の設定
        cameraTransform.transform.LookAt(lookAt);
        //カメラを横にずらして中央を開ける
        cameraTransform.transform.position = cameraTransform.transform.position + cameraTransform.transform.right * SlideDistanceM;

    }
}
