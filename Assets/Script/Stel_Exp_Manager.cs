using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stel_Exp_Manager : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //Tag�� Enemy�� �� Collider�� ��� ���� �۵��ϴ� ����
        if (other.gameObject.tag == "Player")
        {
            Destroy(this);
        }

    }
}
