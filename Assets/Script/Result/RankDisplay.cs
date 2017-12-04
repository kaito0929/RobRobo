using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//============================================
//順位の表示を行うスクリプト
//============================================

public class RankDisplay : MonoBehaviour
{
    // 変数宣言-------------------------------------------------------------------------------------------
    public Image NumberImage;
    public Image RankImage;

    public Sprite[] NumberTexture = new Sprite[4];
    public Sprite[] RankTexture = new Sprite[4];

    public int num;

    private GameObject player;

    // 初期化---------------------------------------------------------------------------------------------
    void Start ()
    {
        player = GameObject.Find("robo(Clone)");
    }
	
	// Update is called once per frame
	void Update ()
    {
        NumberImage.sprite = NumberTexture[player.GetComponent<PlayerPoint>().PlayerRank];
	}
}
