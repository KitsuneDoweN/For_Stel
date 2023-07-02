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
        //PlayerWeapon에서 변수 불러오기
        playerWeapon = GameObject.Find("AttackRange").GetComponent<PlayerWeapon>();
        //Stel_Exp 프리팹 찾기
        stel_Exp = Resources.Load<GameObject>("Prefab/Coin_Test_01_Prefab");
    }

    private void Update()
    {
        Enemy_Death();
    }

    private void OnTriggerStay(Collider other)
    {

        realTime += Time.deltaTime;
        //Tag가 Enemy일 때 Collider와 닿는 동안 작동하는 내용
        if (other.gameObject.tag == "PlayerWeapon")
        {
            //DelayTime 후에 HP가 - 되는 방식
            //지속적으로 받는 데미지의 시간
            if (DelayTime <= realTime)
            {
                //playerWeapon에서 power 변수 가져와서 적용
                Hp -= playerWeapon.power;
                //Hp -= 15.0f;
                realTime = 0;
                //Debug.Log("monster -hp");
            }
        }
    }

    private void Enemy_Death()
    {
        //HP 0 되면 enemy_object 삭제 및 죽은 enemy 위치에 Stel_Exp 생성
        if (Hp <= 0)
        {
            stel_Exp_position = enemy_object.transform.position - new Vector3(0,0.6194374f, 0);
            stel_Exp.transform.position = stel_Exp_position;
            Destroy(enemy_object);
            Instantiate(stel_Exp);
        }
    }
}
