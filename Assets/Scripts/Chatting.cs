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
    public Text chat_text;//ä�� �α�

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
            Debug.Log("����䰡 ��");
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("������Ʈ �Լ� ȣ��");
            if (!chat.isFocused)//ä��â�� Ȱ��ȭ �Ǿ� �ִٸ�
            {
                Debug.Log("���� �Լ� ȣ��");
                send();
                EnablePlayerMovement();
            }
            else              //ä��â�� Ȱ��ȭ �Ǿ� ���� �ʴٸ�
            {
                Debug.Log("ä��â Ȱ��x");
                chat.ActivateInputField();//ä��â�� Ȱ��ȭ
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

            Debug.Log(message+"   �޼���"+PhotonNetwork.NickName);

            pv.RPC("ChatRPC", RpcTarget.AllBuffered, message);
            //������ ��� �޼����� ���ִ� ��� �÷��̾ �� �� ����
            ResetInputField();
        }
        Debug.Log("�Լ� ���������� ȣ�� ���� ����");
    }

    void ResetInputField()
    {
        chat.DeactivateInputField();
        chat.text = "";
        chat.caretPosition = 0; // Ŀ�� ��ġ �ʱ�ȭ
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
            chat_text.text = chat_text.text + "\n" + message; // ä�� �α׿� �޽��� �߰�
        }
        
        ChatScroll scrollHandler = FindObjectOfType<ChatScroll>();
        if (scrollHandler != null)
        {
            scrollHandler.AddMessage(message);
            Debug.Log("��ũ�� ������Ʈ �Լ� ����");
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
