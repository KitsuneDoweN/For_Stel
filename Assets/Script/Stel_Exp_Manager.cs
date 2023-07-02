using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stel_Exp_Manager : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //Tag가 Enemy일 때 Collider와 닿는 동안 작동하는 내용
        if (other.gameObject.tag == "Player")
        {
            Destroy(this);
        }

    }
}
