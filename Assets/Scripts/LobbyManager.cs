using Photon.Pun; // 유니티용 포톤 컴포넌트들
using Photon.Realtime; // 포톤 서비스 관련 라이브러리
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 마스터(매치 메이킹) 서버와 룸 접속을 담당
public class LobbyManager : MonoBehaviourPunCallbacks {
    public InputField user_id;
    public InputField room_name;
    
    public GameObject roomItem;
    public GameObject scrollContents;

    public GameObject ROOM_FULL;
    public GameObject ROOM_EMPTY;

    private Dictionary<string, GameObject> rooms = new Dictionary<string, GameObject>();
    private string gameVersion = "1"; // 게임 버전

    public Text connectionInfoText; // 네트워크 정보를 표시할 텍스트
    public Button joinButton; // 룸 접속 버튼

    // 게임 실행과 동시에 마스터 서버 접속 시도
    private void Start() {
        // 접속에 필요한 정보(게임 버전) 설정
        PhotonNetwork.GameVersion = gameVersion;
        // 설정한 정보를 가지고 마스터 서버 접속 시도
        PhotonNetwork.ConnectUsingSettings();

        // 룸 접속 버튼을 잠시 비활성화
        joinButton.interactable = false;
        // 접속을 시도 중임을 텍스트로 표시
        connectionInfoText.text = "마스터 서버에 접속중...";
    }

    // 마스터 서버 접속 성공시 자동 실행
    public override void OnConnectedToMaster() {
        // 룸 접속 버튼을 활성화
        joinButton.interactable = true;
        // 접속 정보 표시
        connectionInfoText.text = "온라인 : 마스터 서버와 연결됨";
    }

    // 마스터 서버 접속 실패시 자동 실행
    public override void OnDisconnected(DisconnectCause cause) {
        // 룸 접속 버튼을 비활성화
        joinButton.interactable = false;
        // 접속 정보 표시
        connectionInfoText.text = "오프라인 : 마스터 서버와 연결되지 않음\n접속 재시도 중...";

        // 마스터 서버로의 재접속 시도
        PhotonNetwork.ConnectUsingSettings();
    }

    // 룸 접속 시도
    public void Connect() {
        // 중복 접속 시도를 막기 위해, 접속 버튼 잠시 비활성화
        joinButton.interactable = false;

        // 마스터 서버에 접속중이라면
        if (PhotonNetwork.IsConnected)
        {
            // 룸 접속 실행
            connectionInfoText.text = "룸에 접속...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // 마스터 서버에 접속중이 아니라면, 마스터 서버에 접속 시도
            connectionInfoText.text = "오프라인 : 마스터 서버와 연결되지 않음\n접속 재시도 중...";
            // 마스터 서버로의 재접속 시도
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public void makeRoom()
    {
        string room_name = this.room_name.text;
        if(string.IsNullOrEmpty(this.room_name.text) ) 
        {
            Debug.Log("방제목 입력 필요");
            room_name = "Room_" + Random.Range(1, 999).ToString("000");
        }
        PhotonNetwork.NickName = user_id.text;
        PlayerPrefs.SetString("USER_ID",user_id.text);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 4;//나중에 수정 가능
        PhotonNetwork.CreateRoom(room_name, roomOptions, TypedLobby.Default);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("방 만들기 실패 : " + message);
    }


    // (빈 방이 없어)랜덤 룸 참가에 실패한 경우 자동 실행
    public override void OnJoinRandomFailed(short returnCode, string message) {
        // 접속 상태 표시

        if (returnCode == ErrorCode.NoRandomMatchFound) // 32758: 방 없음
        {
            Debug.Log("랜덤 방을 찾을 수 없습니다. 새로운 방을 생성합니다.");
            //CreateNewRoom(); // 방 생성 메서드 호출
            ROOM_EMPTY.active = true;
            //팝업창 활성화
        }
        else if (returnCode == ErrorCode.GameFull) // 32760: 방 가득 참
        {
            Debug.Log("방이 가득 찼습니다. 다른 방을 찾아보세요.");
            //NotifyUserRoomFull(); // 사용자에게 알림 표시
            //팝업창 활성화 
            ROOM_FULL.active = true;
        }
        else
        {
            Debug.LogError($"알 수 없는 오류 발생: {message}");
        }
    }

    // 룸에 참가 완료된 경우 자동 실행
    public override void OnJoinedRoom() {
        // 접속 상태 표시
        connectionInfoText.text = "방 참가 성공";
        // 모든 룸 참가자들이 Main 씬을 로드하게 함
        PhotonNetwork.LoadLevel("Main");

    }
}