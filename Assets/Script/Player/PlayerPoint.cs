using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    //ポイントを表示するImage
    private Image image0;//一の位
    private Image image10;//十の位

    //数字のスプライトを格納する変数
    public Sprite[] spriteArray = new Sprite[10];//配列で10個作る

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

        //シーン上のImageを取得
        image0 = GameObject.Find("PointImage1").GetComponent<Image>();
        image10 = GameObject.Find("PointImage10").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (photonView.isMine)
        {
            //装備毎のポイントを合計する
            myPoint = headPartsMaterialChange.HeadPartsPoint + bodyPartsMaterialChange.BodyPartsPoint +
                  armPartsMaterialChange.ArmPartsPoint + legPartsMaterialChange.LegPartsPoint;

            //画面上のポイント表示用処理--------------------------------------------
            int a = myPoint / 10 % 10;
            int b = myPoint % 10;
            //それぞれの位のImageに数字のスプライトを入れる
            image0.sprite = spriteArray[b];
            image10.sprite = spriteArray[a];
            //----------------------------------------------------------------------


            //リザルト画面でのみ処理するように
            if (SceneManager.GetActiveScene().name == "Result")
            {
                //順位が一番であれば勝利ポーズ
                if (PlayerRank >= 3)
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

    //プレイヤーのポイントを同期
    //順位を決める際の比較に使うので同期しないといけない
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //データの送信
            stream.SendNext(myPoint);
        }
        else
        {
            //データの受信
            this.myPoint = (int)stream.ReceiveNext();
        }
    }
}
