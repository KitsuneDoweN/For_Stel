using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public float Hp = 20;
    public GameObject enemy_object;
    private float DelayTime = 1f;
    private float realTime = 0;

    PlayerWeapon playerWeapon;


    private void Awake()
    {
        //PlayerWeapon���� ���� �ҷ�����
        playerWeapon = GameObject.Find("AttackRange").GetComponent<PlayerWeapon>();
    }

    private void Update()
    {
        Enemy_Death();
    }

    private void OnTriggerStay(Collider other)
    {

        realTime += Time.deltaTime;
        //Tag�� Enemy�� �� Collider�� ��� ���� �۵��ϴ� ����
        if (other.gameObject.tag == "PlayerWeapon")
        {
            //DelayTime �Ŀ� HP�� - �Ǵ� ���
            //���������� �޴� �������� �ð�
            if (DelayTime <= realTime)
            {
                //playerWeapon���� power ���� �����ͼ� ����
                Hp -= playerWeapon.power;
                realTime = 0;
                Debug.Log("monster -hp");
            }
        }
    }

    private void Enemy_Death()
    {
        //HP 0 �Ǹ� ����
        if (Hp <= 0)
        {
            Destroy(enemy_object);
        }
    }
}
