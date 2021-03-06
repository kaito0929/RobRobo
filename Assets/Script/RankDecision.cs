﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//===================================================
//各プレイヤーの得点を比較して順位を決めるスクリプト
//===================================================

public class RankDecision : MonoBehaviour
{
    // 変数宣言-------------------------------------------------------------------------------------------

    //プレイヤーの得点と順位を決めるための変数
    public int[,] num = new int[4, 2];
    //得点の比較を行うかのフラグ
    private bool ChackFlag;

    // 初期化---------------------------------------------------------------------------------------------
    void Start()
    {
        ChackFlag = false;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                num[i, j] = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //シーン上のプレイヤーを取得
        //タグでプレイヤーを識別する
        GameObject Player1 = GameObject.FindGameObjectWithTag("Player1");
        GameObject Player2 = GameObject.FindGameObjectWithTag("Player2");
        GameObject Player3 = GameObject.FindGameObjectWithTag("Player3");
        GameObject Player4 = GameObject.FindGameObjectWithTag("Player4");

        //配列にプレイヤーの順番でポイントを代入
        num[0, 0] = Player1.GetComponent<PlayerPoint>().myPoint;
        num[1, 0] = Player2.GetComponent<PlayerPoint>().myPoint;
        num[2, 0] = Player3.GetComponent<PlayerPoint>().myPoint;
        num[3, 0] = Player4.GetComponent<PlayerPoint>().myPoint;

        //一度だけ処理されるようにする
        if (ChackFlag == false)
        {
            //プレイヤーのポイントを比べてランクを決定
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (num[i, 0] > num[j, 0])
                    {
                        num[i, 1]++;
                    }
                }
            }

            //フラグを操作して一度だけfor文が処理されるようにする
            ChackFlag = true;
        }

        //プレイヤーのランクを代入
        Player1.GetComponent<PlayerPoint>().PlayerRank = num[0, 1];
        Player2.GetComponent<PlayerPoint>().PlayerRank = num[1, 1];
        Player3.GetComponent<PlayerPoint>().PlayerRank = num[2, 1];
        Player4.GetComponent<PlayerPoint>().PlayerRank = num[3, 1];
    }

}
