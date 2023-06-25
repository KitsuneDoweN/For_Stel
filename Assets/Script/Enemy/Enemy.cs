using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //���� Ÿ��(Player)
    GameObject target_player;

    NavMeshAgent nav;
    Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        target_player = GameObject.FindWithTag("Player");
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Ÿ�� ���� ����
        nav.SetDestination(target_player.transform.position);
    }
}
