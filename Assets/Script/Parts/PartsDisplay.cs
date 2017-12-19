using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//======================================
//装備の表示を切り替えるスクリプト
//======================================

public class PartsDisplay : MonoBehaviour
{
    // 変数宣言---------------------------------------------------------------------------------------------
    //実際に表示を切り替えるオブジェクト
    public GameObject HeadParts;
    public GameObject BodyParts;
    public GameObject[] ArmParts = new GameObject[6];
    public GameObject[] LegParts = new GameObject[4];

    //装備をゲットしたかのフラグを持つスクリプト
    public PlayerItemGet playerItemGet;
    //キャラにアタッチされるPhotonViewへの参照
    private PhotonView photonView = null;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    // 初期化-----------------------------------------------------------------------------------------------
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //実際に関数を呼び出して装備の表示を切り替える
        //頭装備-------------------------------------------------------------
        if (playerItemGet.HeadPartsGetFlag == true)
        {
            //フラグがtrueなので装備を表示する方の関数を呼ぶ
            photonView.RPC("HeadPartsDisplay", PhotonTargets.All);
        }
        else
        {
            //フラグがfalseなので装備を非表示にする関数を呼ぶ
            photonView.RPC("HeadPartsHide", PhotonTargets.All);
        }

        //体装備-------------------------------------------------------------
        if (playerItemGet.BodyPartsGetFlag == true)
        {
            photonView.RPC("BodyPartsDisplay", PhotonTargets.All);
        }
        else
        {
            photonView.RPC("BodyPartsHide", PhotonTargets.All);
        }

        //腕装備-------------------------------------------------------------
        if (playerItemGet.ArmPartsGetFlag == true)
        {
            photonView.RPC("ArmPartsDisplay", PhotonTargets.All);
        }
        else
        {
            photonView.RPC("ArmPartsHide", PhotonTargets.All);
        }

        //足装備-------------------------------------------------------------
        if (playerItemGet.LegPartsGetFlag == true)
        {
            photonView.RPC("LegPartsDisplay", PhotonTargets.All);
        }
        else
        {
            photonView.RPC("LegPartsHide", PhotonTargets.All);
        }

    }

    //PunRPCを使って同期を行う
    //装備のSetActiveを切り替えてやって装備している風に見せる
    //頭装備-------------------------------------------------------------
    //装備を表示させる
    [PunRPC]
    private void HeadPartsDisplay()
    {
        HeadParts.SetActive(true);
    }

    [PunRPC]
    private void HeadPartsHide()
    {
        HeadParts.SetActive(false);
    }

    //体装備-------------------------------------------------------------
    [PunRPC]
    private void BodyPartsDisplay()
    {
        BodyParts.SetActive(true);
    }

    [PunRPC]
    private void BodyPartsHide()
    {
        BodyParts.SetActive(false);
    }

    //腕装備-------------------------------------------------------------
    [PunRPC]
    private void ArmPartsDisplay()
    {
        for (int i = 0; i < 6; i++)
        {
            ArmParts[i].SetActive(true);
        }
    }

    [PunRPC]
    private void ArmPartsHide()
    {
        for (int i = 0; i < 6; i++)
        {
            ArmParts[i].SetActive(false);
        }
    }

    //足装備-------------------------------------------------------------
    [PunRPC]
    private void LegPartsDisplay()
    {
        for (int i = 0; i < 4; i++)
        {
            LegParts[i].SetActive(true);
        }
    }

    [PunRPC]
    private void LegPartsHide()
    {
        for (int i = 0; i < 4; i++)
        {
            LegParts[i].SetActive(false);
        }
    }

}
