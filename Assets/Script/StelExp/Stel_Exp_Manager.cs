using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stel_Exp_Manager : MonoBehaviour
{
    public GameObject stel_exp;

    PlayerStatus playerStatus;

    private void Start()
    {   //PlayerStatus 불러오기
        playerStatus = GameObject.Find("Status_").GetComponent<PlayerStatus>();
    }

    private void OnTriggerStay(Collider other)
    {
        //Tag가 Enemy일 때 Collider와 닿는 동안 작동하는 내용
        if (other.gameObject.tag == "Player")
        {
            //PlayerStatus의 경험치 수치 증가
            playerStatus.now_Player_Exp += playerStatus.stel_Exp;
            Destroy(stel_exp);
        }

    }
}
