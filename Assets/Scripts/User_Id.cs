using Photon.Pun;

using UnityEngine;
using UnityEngine.UI;

public class User_Id : MonoBehaviour
{
    public Text userId;
    PhotonView pv = null;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>(); //����� ������Ʈ ������ ����
        userId.text = pv.Owner.NickName; //userId �ؽ�Ʈ�� ���� �г��� �Է�
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
