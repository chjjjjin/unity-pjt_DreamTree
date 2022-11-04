using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PhotonInit : MonoBehaviourPunCallbacks
{
    public string version = "v1.0";
    public InputField userId; //사용자 ID을 입력받는 InputField
    private void Awake()
    {
        PhotonNetwork.GameVersion = version; // 게임의 버전
        PhotonNetwork.ConnectUsingSettings(); //포톤 클라우드에 접속을 시도 
    }
    //포톤 클라우드에 정상적으로 접속이 잘 되었다면 호출되는 콜백함수
    public override void OnConnectedToMaster()
    {
        Debug.Log("Entered Lobby !");
        userId.text = GetUserId();
        //PhotonNetwork.JoinRandomRoom(); //무작위 방에 접속을 시도함
    }
    string GetUserId()//로컬에 저장된 플레이어 이름을 반환하거나, 생성하는 함수
    {
        string userId = PlayerPrefs.GetString("USER_ID"); // 로컬에 저장된 USER_ID를 불러옴
        if (string.IsNullOrEmpty(userId)) //비어있다면
        {
            //USER_ + 0~999사이의 숫자를 userId에 저장
            userId = "USER_" + Random.Range(0, 999).ToString();
        }
        return userId; //userId를 리턴
    }
    //Start버튼을 눌렀을 때 실행할 함수
    public void OnClickJoinRandomRoom()
    {
        //로컬 플레이어의 이름을 userId로 설정
        PhotonNetwork.NickName = userId.text;
        //플레이어 이름을 저장
        PlayerPrefs.SetString("USER_ID", userId.text);
        //방 접속 시도 
        PhotonNetwork.JoinRandomRoom();
    }
    //포톤 클라우드에 접속에 실패했을 때 호출되는 콜백 함수
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Connect Error");
        PhotonNetwork.ConnectUsingSettings(); //포톤 클라우드에 접속을 재시도 
    }
    //무작위 룸 접속에 실패한 경우 호출되는 콜백 함수 
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No rooms!");
        PhotonNetwork.CreateRoom("My room", new RoomOptions { MaxPlayers = 6 });
    }
    public override void OnJoinedRoom() // 룸에 입학하면 호출되는 콜백 함수
    {
        Debug.Log("Enter Room");
        //  CreateTank(); //UFO Bunny를 네트워크 공간에 생성
        StartCoroutine(LoadPlay()); //룸씬으로 이동하는 코루틴 실행
    }
    IEnumerator LoadPlay()
    {
        //씬을 이동하는 동안 포톨 클라우드의 서버로부터 네트워크 메시지 수신 중단
        PhotonNetwork.IsMessageQueueRunning = false;
        //백그라운드로 씬 로딩
        AsyncOperation ao = Application.LoadLevelAsync("Play");
        //PhotonNetwork.LoadLevel() : 메시지 수신을 중단 후 씬을 전환하는 함수
        yield return ao;
    }

    //void CreateBunny()
    //{
    //    float pos = Random.Range(-100.0f, 100.0f);
    //    PhotonNetwork.Instantiate("Tank", new Vector3(pos, 20.0f, pos), Quaternion.identity, 0);
    //}
}
