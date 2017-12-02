﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//============================================
//足装備のマテリアルを切り替えるスクリプト
//============================================

public class LegPartsMaterialChange : MonoBehaviour
{

    // 変数宣言---------------------------------------------------------------------------------------------

    //装備品のマテリアル
    //配列変数で宣言してそれぞれのランクの色のマテリアルをアタッチ
    //数値を切り替えることで装備の見た目を変えることが出来る
    public Material[] PartsMaterial = new Material[3];

    //マテリアルを切り替える数値
    public int MaterialNumber;

    //足装備に振り分けられたポイント
    public int LegPartsPoint;

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
    void Update()
    {
        if (photonView.isMine)
        {
            GetComponent<Renderer>().material = PartsMaterial[MaterialNumber];

            ////装備の状態に応じてスコアを変更
            switch (MaterialNumber)
            {
                case 0:
                    LegPartsPoint = 1;
                    break;
                case 1:
                    LegPartsPoint = 2;
                    break;
                case 2:
                    LegPartsPoint = 3;
                    break;
            }
        }
    }
}
