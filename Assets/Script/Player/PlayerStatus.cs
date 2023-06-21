using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //Player Hp
    public float Hp = 60;
    private float DelayTime = 1.5f;
    private float realTime = 0;

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
                Debug.Log("player-hp");
            }
        }

    }

}
