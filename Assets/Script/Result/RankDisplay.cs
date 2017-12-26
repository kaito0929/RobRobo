using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//============================================
//順位の表示を行うスクリプト
//============================================

public class RankDisplay : MonoBehaviour
{
    // 変数宣言-------------------------------------------------------------------------------------------

    //PlayerPoint参照用変数
    public PlayerPoint playerPoint;

    //順位表示用の画像を扱うための変数
    public Sprite[] image=new Sprite[7];

    //画像を表示するためのオブジェクト
    private GameObject NumberImage;
    private GameObject RankImage;


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
    void Update ()
    {
        if (photonView.isMine)
        {
            //リザルト画面でのみ処理を行うようにする
            if (SceneManager.GetActiveScene().name == "Result")
            {
                //シーン上のNumberImageオブジェクトを取得
                NumberImage = GameObject.Find("NumberImage");
                //画像の座標を変更
                NumberImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(-620, -241, 0);
                //画像の大きさを決定
                NumberImage.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0.7f);
                //アスペクト比
                NumberImage.GetComponent<Image>().preserveAspect = true;
                NumberImage.GetComponent<Image>().SetNativeSize();

                //コメントは上記を参照してください
                RankImage = GameObject.Find("RankImage");
                RankImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(-446, -241, 0);
                RankImage.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0.7f);
                RankImage.GetComponent<Image>().preserveAspect = true;
                RankImage.GetComponent<Image>().SetNativeSize();

                //PlayerRankの数値に応じて表示する画像を変更
                switch (playerPoint.PlayerRank)
                {
                    case 3://一位
                        NumberImage.GetComponent<Image>().sprite = image[3];
                        RankImage.GetComponent<Image>().sprite = image[0];
                        break;
                    case 2://二位
                        NumberImage.GetComponent<Image>().sprite = image[4];
                        RankImage.GetComponent<Image>().sprite = image[1];
                        break;
                    case 1://三位
                        NumberImage.GetComponent<Image>().sprite = image[5];
                        RankImage.GetComponent<Image>().sprite = image[2];
                        break;
                    case 0://四位
                        NumberImage.GetComponent<Image>().sprite = image[6];
                        RankImage.GetComponent<Image>().sprite = image[2];
                        break;
                }


            }
        }


    }

}
