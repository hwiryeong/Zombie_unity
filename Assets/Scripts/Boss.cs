using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : LivingEntity
{
    public GameObject ranking;

    //죽을떄 랭킹을 켜야함
    //랭킹창에서 확인을 누르면 로비로 나가져야함

    //생성은 게임메니져와 좀비스포너에서 조절 필요
    //


    //로직은 좀비와 비슷하나 주기적으로 목표물을 바꿔야함
    //체력, 이동속도, 공격력등은 기존에 사용한 데이터를 기반으로 하며 네트워크 객체로 생성시
    //플레이어 수를 기반으로 체력을늘림(가변하지 않음)
    //필요한 메소드 공격/데미지 받을떄/죽을때/몬스터를 소환(어떨떄? 내부 쿨이 필요함)
    //로직은 대부분 좀비에 있는것을 그대로 사용




}