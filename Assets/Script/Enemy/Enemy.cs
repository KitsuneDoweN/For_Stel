using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //따라갈 타겟(Player)
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
        //타겟 따라 가기
        nav.SetDestination(target_player.transform.position);
    }
}
