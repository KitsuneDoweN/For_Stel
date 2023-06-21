using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public float Hp = 20;
    private float DelayTime = 1f;
    private float realTime = 0;

    PlayerWeapon playerWeapon;

    private void Awake()
    {
        //PlayerWeapon에서 변수 불러오기
        playerWeapon = GameObject.Find("AttackRange").GetComponent<PlayerWeapon>();
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
                realTime = 0;
                Debug.Log("monster -hp");
            }
        }


    }

}
