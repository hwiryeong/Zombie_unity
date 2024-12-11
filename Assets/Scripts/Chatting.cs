using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class Chatting : MonoBehaviour
{
    public InputField chat;
    public Text chat_text;//채팅 로그

    PhotonView pv;
    PlayerMovement playerMovement;


    private void Awake()
    {
        if(pv == null)
            pv = GameManager.instance.photonView;

        if (pv.IsMine)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }
    }

    void Update()
    {
        if (pv == null)
            Debug.Log("포톤뷰가 널");
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("업데이트 함수 호출");
            if (!chat.isFocused)//채팅창이 활성화 되어 있다면
            {
                Debug.Log("샌드 함수 호출");
                send();
                EnablePlayerMovement();
            }
            else              //채팅창이 활성화 되어 있지 않다면
            {
                Debug.Log("채팅창 활성x");
                chat.ActivateInputField();//채팅창을 활성화
                DisablePlayerMovement();
            }
       
        }          
    }

    void send()
    {

        if(!string.IsNullOrEmpty(chat.text))
        {
            string message = "<color=#00ff00>" +
             PhotonNetwork.NickName + " : " + this.chat.text +"</color>";

            Debug.Log(message+"   메세지"+PhotonNetwork.NickName);

            pv.RPC("ChatRPC", RpcTarget.AllBuffered, message);
            //과거의 모든 메세지를 방있는 모든 플레이어가 볼 수 있음
            ResetInputField();
        }
        Debug.Log("함수 정상적으로 호출 되지 않음");
    }

    void ResetInputField()
    {
        chat.DeactivateInputField();
        chat.text = "";
        chat.caretPosition = 0; // 커서 위치 초기화
        chat.ActivateInputField();
    }

    [PunRPC]
    void ChatRPC(string message)
    {
        if (chat_text.text.IsNullOrEmpty())
        {
            chat_text.text = message;
        }
        else
        {
            chat_text.text = chat_text.text + "\n" + message; // 채팅 로그에 메시지 추가
        }
        
        ChatScroll scrollHandler = FindObjectOfType<ChatScroll>();
        if (scrollHandler != null)
        {
            scrollHandler.AddMessage(message);
            Debug.Log("스크롤 업데이트 함수 실행");
        }
    }

    void EnablePlayerMovement()
    {
        if (playerMovement != null && !playerMovement.enabled)
        {
            playerMovement.enabled = true;
        }
    }
    void DisablePlayerMovement()
    {
        if (playerMovement != null && playerMovement.enabled)
        {
            playerMovement.enabled = false;
        }
    }
}
