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
    public PlayerPoint playerPoint;

    public Sprite[] image=new Sprite[7];
    GameObject NumberImage;
    GameObject RankImage;

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

            if (SceneManager.GetActiveScene().name == "Result")
            {
                NumberImage = GameObject.Find("NumberImage");
                NumberImage.transform.parent = GameObject.Find("Canvas").transform;
                NumberImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(-620, -241, 0);
                NumberImage.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0.7f);
                NumberImage.GetComponent<Image>().preserveAspect = true;
                NumberImage.GetComponent<Image>().SetNativeSize();

                RankImage = GameObject.Find("RankImage");
                RankImage.transform.parent = GameObject.Find("Canvas").transform;
                RankImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(-446, -241, 0);
                RankImage.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0.7f);
                RankImage.GetComponent<Image>().preserveAspect = true;
                RankImage.GetComponent<Image>().SetNativeSize();

                switch (playerPoint.PlayerRank)
                {
                    case 0:
                        NumberImage.GetComponent<Image>().sprite = image[3];
                        RankImage.GetComponent<Image>().sprite = image[0];
                        break;
                    case 1:
                        NumberImage.GetComponent<Image>().sprite = image[4];
                        RankImage.GetComponent<Image>().sprite = image[1];
                        break;
                }


            }
        }


    }

}
