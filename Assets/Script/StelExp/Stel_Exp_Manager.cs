using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stel_Exp_Manager : MonoBehaviour
{
    public GameObject stel_exp;
    PlayerStatus playerStatus;

    private void Start()
    {   //PlayerStatus �ҷ�����
        playerStatus = GameObject.Find("Status_").GetComponent<PlayerStatus>();
        
    }

    private void OnTriggerStay(Collider other)
    {
        //Tag�� Enemy�� �� Collider�� ��� ���� �۵��ϴ� ����
        if (other.gameObject.tag == "Player")
        {
            //PlayerStatus�� ����ġ ��ġ ����
            playerStatus.now_Player_Exp += playerStatus.stel_Exp;
            Destroy(stel_exp);
        }

    }
}
