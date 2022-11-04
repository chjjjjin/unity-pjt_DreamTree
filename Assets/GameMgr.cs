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
        //PhotonNetwork.GameVersion = version;  // ������ ����
        //if (!PhotonNetwork.IsConnected)  // ���� ������ �����ִٸ�
        //{
        //    PhotonNetwork.ConnectUsingSettings(); // ���� Ŭ���忡 ������ �õ� 
        //}

        //userId.text = GetUserId();
        CreateBunny();  // ����
        //����Ŭ����� ��Ʈ��ũ �޽��� ������ �ٽ� ����
        PhotonNetwork.IsMessageQueueRunning = true;

        ////GetConnectPlayerCount();
        //pv = GetComponent<PhotonView>();  // PhotonView ������Ʈ�� �Ҵ�
    }

    void CreateBunny()
    {
        float posx = Random.Range(-17.29f, 0.67f);
        float posz = Random.Range(-14.21f, -25.7f);
        // PhotonNetwork�� �������� ����
        PhotonNetwork.Instantiate("UFO Bunny", new Vector3(posx, 2.8f, posz), Quaternion.identity, 0);
    }
}
