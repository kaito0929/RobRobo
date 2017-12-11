using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//===========================================
//ゲームの残り時間を計測するスクリプト
//時間に応じて装備の表示を行う
//===========================================

public class TimeCount : MonoBehaviour
{
    // 変数宣言---------------------------------------------------------------------------------------------
    //ゲームの残り時間
    private float GameTime;

    //時間を表示するImage
    public Image image0;//一の位
    public Image image10;//十の位
    public Image image100;//百の位

    //数字のスプライトを格納する変数
    public Sprite[] spriteArray = new Sprite[10];//配列で10個作る

    //フィールド上に落ちている装備
    //銅装備
    public GameObject[] ThirdRanKParts = new GameObject[12];
    //銀装備
    public GameObject[] SeconeRanKParts = new GameObject[8];
    //金装備
    public GameObject[] FirstRanKParts = new GameObject[4];

    //SceneInitialized参照用変数
    public SceneInitialized sceneInit;

    //タイムアップになったかのフラグ
    public bool TimeUpFlag;

    //装備を表示させるタイミングを計るための変数
    int num;
    int count;

    //アタッチされるPhotonViewへの参照
    private PhotonView photonView = null;
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    //  初期化---------------------------------------------------------------------------------------------
    void Start ()
    {
        DontDestroyOnLoad(gameObject);

        GameTime = 180;
        num = 180;
        TimeUpFlag = false;
	}

    // Update is called once per frame
    void Update()
    {
        int a = (int)GameTime / 100 % 10;
        int b = (int)GameTime / 10 % 10;
        int c = (int)GameTime % 10;

        image0.sprite = spriteArray[c];//一の位のイメージののスプライトに数字のスプライトをぶちこむ
        image10.sprite = spriteArray[b];//十の位のイメージのスプライトに数字のスプライトをぶち込む
        image100.sprite = spriteArray[a];//百の位のイメージのスプライトに数字のスプライトをぶち込む

        //全員シーン遷移が行われたかのフラグで処理
        if (sceneInit.SceneChangeFlag == true)
        {
            if (GameTime >= 0.0f)
            {
                //残り時間を計測
                GameTime -= Time.deltaTime;
            }

            //装備表示のタイミングを計る
            count++;
            if (count >= 60)
            {
                num--;
                count = 0;
            }
        }

        //残り時間が0以下になった場合に処理
        if (GameTime <= 0.0f)
        {
            //フラグをfalseにして画面遷移が開始する
            TimeUpFlag = true;
        }

        photonView.RPC("test", PhotonTargets.All);
    }

    [PunRPC]
    void test()
    {
        if (num == 180)
        {
            ThirdRanKParts[0].SetActive(true);
            ThirdRanKParts[1].SetActive(true);
            ThirdRanKParts[2].SetActive(true);
            ThirdRanKParts[3].SetActive(true);
            ThirdRanKParts[4].SetActive(true);
            ThirdRanKParts[5].SetActive(true);
            ThirdRanKParts[6].SetActive(true);
            ThirdRanKParts[7].SetActive(true);
        }
        if (num == 150)
        {
            ThirdRanKParts[8].SetActive(true);
            ThirdRanKParts[9].SetActive(true);
            SeconeRanKParts[0].SetActive(true);
            SeconeRanKParts[1].SetActive(true);
        }
        if (num == 120)
        {
            ThirdRanKParts[10].SetActive(true);
            ThirdRanKParts[11].SetActive(true);
            SeconeRanKParts[2].SetActive(true);
            FirstRanKParts[0].SetActive(true);
        }
        if (num == 90)
        {
            SeconeRanKParts[3].SetActive(true);
            SeconeRanKParts[4].SetActive(true);
            SeconeRanKParts[5].SetActive(true);
        }
        if (num == 60)
        {
            SeconeRanKParts[6].SetActive(true);
            SeconeRanKParts[7].SetActive(true);
            FirstRanKParts[1].SetActive(true);
        }
        if (num == 30)
        {
            FirstRanKParts[2].SetActive(true);
            FirstRanKParts[3].SetActive(true);
        }
    }


    //変数の同期
    void OnPhotonSerializeView(PhotonStream i_stream, PhotonMessageInfo i_info)
    {
        if (i_stream.isWriting)
        {
            //データの送信
            i_stream.SendNext(GameTime);
            i_stream.SendNext(TimeUpFlag);
        }
        else
        {
            //データの受信
            GameTime = (float)i_stream.ReceiveNext();
            TimeUpFlag = (bool)i_stream.ReceiveNext();
        }
    }

}
