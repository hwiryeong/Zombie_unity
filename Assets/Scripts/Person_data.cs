using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person_data : MonoBehaviourPun
{
    public int killCount;
    public int damage;

    private void Awake()
    {
        killCount = 0;
        damage = 0;
    }
    public void AddKill()
    {
            this.killCount++;
   
    }

    public void AddDamage(int damage)
    {
        
            this.damage += damage;
        
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 로컬 오브젝트라면 쓰기 부분이 실행됨
        if (stream.IsWriting)
        {

            stream.SendNext(killCount);
            stream.SendNext(damage);

        }
        else
        {
            killCount = (int)stream.ReceiveNext();
            damage = (int)stream.ReceiveNext();
        }
    }
}
