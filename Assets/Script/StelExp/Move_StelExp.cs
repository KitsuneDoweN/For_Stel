using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move_StelExp : MonoBehaviour
{
    NavMeshAgent nav;
    GameObject exp_target_player;

    // Start is called before the first frame update
    void Start()
    {
        exp_target_player = GameObject.FindWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            nav.SetDestination(exp_target_player.transform.position);
        }

    }
}
