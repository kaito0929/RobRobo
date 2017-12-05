using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{

    private float GameTime;

    public Image image0;
    public Image image10;
    public Image image100;

    public Sprite[] spriteArray = new Sprite[10];//配列で10個作る

    public GameObject[] ThirdRanKParts = new GameObject[12];
    public GameObject[] SeconeRanKParts = new GameObject[8];
    public GameObject[] FirstRanKParts = new GameObject[4];

    //キャラにアタッチされるPhotonViewへの参照
    private PhotonView photonView = null;
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }


    // Use this for initialization
    void Start ()
    {
        GameTime = 180;
	}
	
	// Update is called once per frame
	void Update ()
    {
        int a = (int)GameTime / 100 % 10;
        int b = (int)GameTime / 10 % 10;
        int c = (int)GameTime % 10;

        image0.sprite = spriteArray[c];//一の位のイメージののスプライトに数字のスプライトをぶちこむ
        image10.sprite = spriteArray[b];//十の位のイメージのスプライトに数字のスプライトをぶち込む
        image100.sprite = spriteArray[a];//百の位のイメージのスプライトに数字のスプライトをぶち込む

        GameTime -= Time.deltaTime;

        photonView.RPC("test", PhotonTargets.All);
    }

    [PunRPC]
    void test()
    {
        if (GameTime == 180)
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
        if (GameTime == 150)
        {
            ThirdRanKParts[8].SetActive(true);
            ThirdRanKParts[9].SetActive(true);
            SeconeRanKParts[0].SetActive(true);
            SeconeRanKParts[1].SetActive(true);
        }
        if (GameTime == 120)
        {
            ThirdRanKParts[10].SetActive(true);
            ThirdRanKParts[11].SetActive(true);
            SeconeRanKParts[2].SetActive(true);
            FirstRanKParts[0].SetActive(true);
        }
        if (GameTime == 90)
        {
            SeconeRanKParts[3].SetActive(true);
            SeconeRanKParts[4].SetActive(true);
            SeconeRanKParts[5].SetActive(true);
        }
        if (GameTime == 60)
        {
            SeconeRanKParts[6].SetActive(true);
            SeconeRanKParts[7].SetActive(true);
            FirstRanKParts[1].SetActive(true);
        }
        if (GameTime == 30)
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
        }
        else
        {
            //データの受信
            GameTime = (float)i_stream.ReceiveNext();
        }
    }

}
