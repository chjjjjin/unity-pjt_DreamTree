using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PhotonInit : MonoBehaviourPunCallbacks
{
    public string version = "v1.0";
    public InputField userId; //����� ID�� �Է¹޴� InputField
    private void Awake()
    {
        PhotonNetwork.GameVersion = version; // ������ ����
        PhotonNetwork.ConnectUsingSettings(); //���� Ŭ���忡 ������ �õ� 
    }
    //���� Ŭ���忡 ���������� ������ �� �Ǿ��ٸ� ȣ��Ǵ� �ݹ��Լ�
    public override void OnConnectedToMaster()
    {
        Debug.Log("Entered Lobby !");
        userId.text = GetUserId();
        //PhotonNetwork.JoinRandomRoom(); //������ �濡 ������ �õ���
    }
    string GetUserId()//���ÿ� ����� �÷��̾� �̸��� ��ȯ�ϰų�, �����ϴ� �Լ�
    {
        string userId = PlayerPrefs.GetString("USER_ID"); // ���ÿ� ����� USER_ID�� �ҷ���
        if (string.IsNullOrEmpty(userId)) //����ִٸ�
        {
            //USER_ + 0~999������ ���ڸ� userId�� ����
            userId = "USER_" + Random.Range(0, 999).ToString();
        }
        return userId; //userId�� ����
    }
    //Start��ư�� ������ �� ������ �Լ�
    public void OnClickJoinRandomRoom()
    {
        //���� �÷��̾��� �̸��� userId�� ����
        PhotonNetwork.NickName = userId.text;
        //�÷��̾� �̸��� ����
        PlayerPrefs.SetString("USER_ID", userId.text);
        //�� ���� �õ� 
        PhotonNetwork.JoinRandomRoom();
    }
    //���� Ŭ���忡 ���ӿ� �������� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Connect Error");
        PhotonNetwork.ConnectUsingSettings(); //���� Ŭ���忡 ������ ��õ� 
    }
    //������ �� ���ӿ� ������ ��� ȣ��Ǵ� �ݹ� �Լ� 
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No rooms!");
        PhotonNetwork.CreateRoom("My room", new RoomOptions { MaxPlayers = 6 });
    }
    public override void OnJoinedRoom() // �뿡 �����ϸ� ȣ��Ǵ� �ݹ� �Լ�
    {
        Debug.Log("Enter Room");
        //  CreateTank(); //UFO Bunny�� ��Ʈ��ũ ������ ����
        StartCoroutine(LoadPlay()); //������� �̵��ϴ� �ڷ�ƾ ����
    }
    IEnumerator LoadPlay()
    {
        //���� �̵��ϴ� ���� ���� Ŭ������ �����κ��� ��Ʈ��ũ �޽��� ���� �ߴ�
        PhotonNetwork.IsMessageQueueRunning = false;
        //��׶���� �� �ε�
        AsyncOperation ao = Application.LoadLevelAsync("Play");
        //PhotonNetwork.LoadLevel() : �޽��� ������ �ߴ� �� ���� ��ȯ�ϴ� �Լ�
        yield return ao;
    }

    //void CreateBunny()
    //{
    //    float pos = Random.Range(-100.0f, 100.0f);
    //    PhotonNetwork.Instantiate("Tank", new Vector3(pos, 20.0f, pos), Quaternion.identity, 0);
    //}
}
