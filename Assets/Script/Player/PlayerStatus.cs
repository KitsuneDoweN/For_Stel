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

    //������ �� ����ġ �ý���
    public float Level = 1;
    private float Levelup = 1;
    public float Start_Player_Exp = 0;
    public float now_Player_Exp = 0;
    public float Max_Player_Exp = 100;
    public float stel_Exp = 10;

    //������Ÿ��
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
        //���� ����ġ�� �ִ�ġ�� �Ǿ��� �� ������ �� ���� ����ġ �ʱ�ȭ
        if(now_Player_Exp >= Max_Player_Exp)
        {
            Level += Levelup;
            now_Player_Exp = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        realTime += Time.deltaTime;
        //Tag�� Enemy�� �� Collider�� ��� ���� �۵��ϴ� ����
        if (other.gameObject.tag == "Enemy")
        {
            //DelayTime �Ŀ� HP�� - �Ǵ� ���
            //���������� �޴� �������� �ð�
            if (DelayTime <= realTime)
            {
                Hp -= 1;
                realTime = 0;
            }
        }

    }

    private void Player_Death()
    {
        //HP 0 �Ǹ� ����
        if (Hp <= 0)
        {
            Destroy(player_object);
        }
    }

}
