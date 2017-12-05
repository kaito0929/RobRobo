using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//===========================================
//マッチング画面での残りの必要人数の表示
//===========================================

public class PlayerNumDisplay : MonoBehaviour
{
    // 変数宣言---------------------------------------------------------------------------------------------
    //表示を切り替えるシーン上のImage
    public Image NumImage;
    //表示する数字のスプライト
    public Sprite[] NumSprite = new Sprite[3];

    //残りの必要人数の変数を持ったスクリプト参照用
    public GameStart gameStart;

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        //残りの必要人数に応じてスプライトを切り替える
        switch (gameStart.GetPlayerNecessaryNum())
        {
            case 3:
                NumImage.sprite = NumSprite[2];
                break;
            case 2:
                NumImage.sprite = NumSprite[1];
                break;
            case 1:
                NumImage.sprite = NumSprite[0];
                break;
        }
    }
}
