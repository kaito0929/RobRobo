using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnd : MonoBehaviour
{
    public bool EndFlag;
	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject);
        EndFlag = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            EndFlag = true;
        }
    }

    //プレイヤーの数を同期
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //データの送信
            stream.SendNext(EndFlag);
        }
        else
        {
            //データの受信
            this.EndFlag = (bool)stream.ReceiveNext();
        }
    }

}
