using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSceneChange : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        //エンターキーを押したら処理
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PhotonNetwork.LeaveRoom();
            
        }
    }

    public void OnLeftRoom()
    {
        SceneManager.LoadScene("Title");
    }
}
