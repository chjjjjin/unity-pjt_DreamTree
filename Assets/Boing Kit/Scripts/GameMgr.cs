using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameMgr : MonoBehaviourPunCallbacks
{
    public string version = "v1.0";
    public PhotonView pv;
    // Start is called before the first frame update
    private void Awake()
    {
        PhotonNetwork.GameVersion = version;
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        
        

        pv = GetComponent<PhotonView>();


    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Entered Lobby !");
        //userId.text = GetUserId();
        PhotonNetwork.CreateRoom("My room", new RoomOptions { MaxPlayers = 20 });

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Enter Room");
        CreateBunny();
        //StartCoroutine(LoadBattleField());
    }

    void CreateBunny()
    {
        float posx = Random.Range(-17.44f, 1.48f);
        float posz = Random.Range(-12.33f, -21.38f);
        PhotonNetwork.Instantiate("UFO Bunny 1", new Vector3(posx, 2.8f, posz), Quaternion.identity, 0);
    }
}
