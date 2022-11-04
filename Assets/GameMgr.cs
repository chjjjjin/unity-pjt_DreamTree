using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class GameMgr : MonoBehaviourPunCallbacks
{
    private PhotonView pv;
    public string version = "v1.0";

    // Start is called before the first frame update
    void Awake()
    {
        //PhotonNetwork.GameVersion = version;  // 게임의 버전
        //if (!PhotonNetwork.IsConnected)  // 포톤 연결이 끊겨있다면
        //{
        //    PhotonNetwork.ConnectUsingSettings(); // 포톤 클라우드에 접속을 시도 
        //}

        //userId.text = GetUserId();
        CreateBunny();  // 생성
        //포톤클라우드와 네트워크 메시지 수신을 다시 연결
        PhotonNetwork.IsMessageQueueRunning = true;

        ////GetConnectPlayerCount();
        //pv = GetComponent<PhotonView>();  // PhotonView 컴포넌트를 할당
    }

    void CreateBunny()
    {
        float posx = Random.Range(-17.29f, 0.67f);
        float posz = Random.Range(-14.21f, -25.7f);
        // PhotonNetwork로 프리팹을 생성
        PhotonNetwork.Instantiate("UFO Bunny", new Vector3(posx, 2.8f, posz), Quaternion.identity, 0);
    }
}
