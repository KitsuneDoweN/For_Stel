using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //Player Hp
    public float Hp = 0;
    public float MaxHp = 60;
    public GameObject player_object;

    //레벨업 및 경험치 시스템
    public float Level = 1;
    private float Levelup = 1;
    public float Start_Player_Exp = 0;
    public float now_Player_Exp = 0;
    public float Max_Player_Exp = 100;
    public float stel_Exp = 10;

    //딜레이타임
    private float DelayTime = 1.5f;
    private float realTime = 0;

    private void Start()
    {
        Hp = MaxHp;
    }

    private void Update()
    {
        Player_Death();
        Player_LevelUp();
    }

    private void Player_LevelUp()
    {
        //현재 경험치가 최대치에 되었을 때 레벨업 및 현재 경험치 초기화
        if(now_Player_Exp >= Max_Player_Exp)
        {
            Level += Levelup;
            now_Player_Exp = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        realTime += Time.deltaTime;
        //Tag가 Enemy일 때 Collider와 닿는 동안 작동하는 내용
        if (other.gameObject.tag == "Enemy")
        {
            //DelayTime 후에 HP가 - 되는 방식
            //지속적으로 받는 데미지의 시간
            if (DelayTime <= realTime)
            {
                Hp -= 1;
                realTime = 0;
            }
        }

    }

    private void Player_Death()
    {
        //HP 0 되면 삭제
        if (Hp <= 0)
        {
            Destroy(player_object);
        }
    }

}
