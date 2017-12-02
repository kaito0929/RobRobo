using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==============================================
//装備品のランクを決めるスクリプト
//==============================================

public class ItemRank : MonoBehaviour
{
    // 変数宣言---------------------------------------------------------------------------------------------

    //装備品のマテリアル
    //配列変数で宣言してそれぞれのランクの色のマテリアルをアタッチ
    //数値を切り替えることで装備の見た目を変えることが出来る
    public Material[] PartsMaterial = new Material[3];

    //装備品のマテリアルを切り替えるための変数
    //装備品のランクやポイントにも関係する
    public int MaterialNumber;

    // 初期化-----------------------------------------------------------------------------------------------
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //実際に装備のマテリアルを切り替える
        GetComponent<Renderer>().material = PartsMaterial[MaterialNumber];
	}
}
