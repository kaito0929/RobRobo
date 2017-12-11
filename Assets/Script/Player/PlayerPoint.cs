using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//==============================================
//プレイヤーの総合得点と順位を決めるスクリプト
//==============================================

public class PlayerPoint : MonoBehaviour
{
    // 変数宣言---------------------------------------------------------------------------------------------

    //PartsMaterialChangeスクリプト参照用変数
    //頭装備
    public HeadPartsMaterialChange headPartsMaterialChange;
    //体装備
    public BodyPartsMaterialChange bodyPartsMaterialChange;
    //腕装備
    public ArmPartsMaterialChange armPartsMaterialChange;
    //足装備
    public LegPartsMaterialChange legPartsMaterialChange;

    //プレイヤーのポイント
    public int myPoint;
    //プレイヤーの順位
    public int PlayerRank;

    //キャラにアタッチされるアニメーターへの参照
    private Animator anim;

    //キャラにアタッチされるPhotonViewへの参照
    private PhotonView photonView = null;
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }


    // Use this for initialization
    void Start ()
    {
        PlayerRank = 0;
        //Animatorコンポーネントを取得する
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (photonView.isMine)
        {
            //装備毎のポイントを合計する
            myPoint = headPartsMaterialChange.HeadPartsPoint + bodyPartsMaterialChange.BodyPartsPoint +
                  armPartsMaterialChange.ArmPartsPoint + legPartsMaterialChange.LegPartsPoint;

            //リザルト画面でのみ処理するように
            if (SceneManager.GetActiveScene().name == "Result")
            {
                //順位が一番であれば勝利ポーズ
                if (PlayerRank == 3)
                {
                    anim.SetBool("Victory", true);
                }
                //一位じゃなかったら拍手のモーション
                else
                {
                    anim.SetBool("Lose", true);
                }
            }
        }
	}
}
