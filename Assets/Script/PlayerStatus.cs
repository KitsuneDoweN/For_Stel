using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //Player Hp
    public float Hp = 60;
    private float DelayTime = 0.5f;
    private float realTime = 0;

    private void OnTriggerEnter(Collider other)
    {
        realTime = Time.deltaTime;
       
        if (other.gameObject.tag == "Enemy") { 
            
            if (DelayTime >= realTime)
            {
                Hp -= 1;
                realTime = 0;
                Debug.Log("-*");
            }
        }
        //Tag가 Enemy일 때 Collider와 닿으면 작동하는 내용
        
    }

}
