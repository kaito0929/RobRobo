using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//============================================
//相手プレイヤーのロケットパンチが自キャラに当たった際の処理
//攻撃を喰らったモーションを再生する
//============================================

public class PunchHit : MonoBehaviour
{
    // 変数宣言---------------------------------------------------------------------------------------------

    //キャラにアタッチされるアニメーターへの参照
    private Animator anim;
    //相手のロケットパンチが当たった時のフラグ
    public bool PunchHitFlag;
    //無敵時間
    //アニメーションの再生にも関係している
    private float InvincibleCount;

    public GameObject myPunch;

    //キャラにアタッチされるPhotonViewへの参照
    private PhotonView photonView = null;
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    // 初期化---------------------------------------------------------------------------------------------
    void Start ()
    {
        //Animatorコンポーネントを取得する
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update ()
    {
        //持ち主でないのなら制御させない
        if (!photonView.isMine)
        {
            return;
        }

        //くらいアニメーションの再生
        //PunchHitFlagでアニメーションが切り替わるように
        anim.SetBool("Hit", PunchHitFlag);

        //PunchHitFlagがtrue、つまり攻撃を喰らった場合に処理
        if(PunchHitFlag==true)
        {
            //無敵時間のカウントを開始
            InvincibleCount += Time.deltaTime;
        }

        //無敵時間が2.0fを超えたならば処理
        if(InvincibleCount >= 2.0f)
        {
            //フラグをfalseにしてアニメーションを切り替える
            PunchHitFlag = false;
            //カウントをリセット
            //本来はもう少し後でリセットさせる
            InvincibleCount = 0.0f;
        }
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
                if (other.gameObject != myPunch)
                {
                    //PunchHitFlagがfalseだった場合に処理される
                    if (PunchHitFlag == false)
                    {
                        //フラグをtrueにしてアニメーションを再生させる
                        PunchHitFlag = true;
                    }
                }
            }
        }
    }

}
