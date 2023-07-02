using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public float Hp = 20;
    public GameObject enemy_object;

    public GameObject stel_Exp;

    private float DelayTime = 1f;
    private float realTime = 0;

    PlayerWeapon playerWeapon;

    Vector3 stel_Exp_position;

    private void Start()
    {
        //PlayerWeapon���� ���� �ҷ�����
        playerWeapon = GameObject.Find("AttackRange").GetComponent<PlayerWeapon>();
        //Stel_Exp ������ ã��
        stel_Exp = Resources.Load<GameObject>("Prefab/Coin_Test_01_Prefab");
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
                //Hp -= 15.0f;
                realTime = 0;
                //Debug.Log("monster -hp");
            }
        }
    }

    private void Enemy_Death()
    {
        //HP 0 �Ǹ� enemy_object ���� �� ���� enemy ��ġ�� Stel_Exp ����
        if (Hp <= 0)
        {
            stel_Exp_position = enemy_object.transform.position;
            stel_Exp.transform.position = stel_Exp_position;
            Destroy(enemy_object);
            Instantiate(stel_Exp);
        }
    }
}
