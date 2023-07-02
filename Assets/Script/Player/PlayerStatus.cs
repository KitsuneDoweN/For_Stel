using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //Player Hp
    public float Hp = 60;
    public GameObject player_object;

    private float DelayTime = 1.5f;
    private float realTime = 0;


    private void Update()
    {
        Player_Death();
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
                //Debug.Log("player-hp");
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
