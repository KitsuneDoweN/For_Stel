using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Spawner : MonoBehaviour
{
    private float realTime;
    public GameObject enemy_Object;


    // Update is called once per frame
    void Update()
    {
        realTime += Time.deltaTime;

        if(realTime >= 2)
        {
            //랜덤 스폰 범위
            float m_X = Random.Range(-250f, 250f);
            float m_Y = Random.Range(1, 1);
            float m_Z = Random.Range(-250f, 250f);

            Instantiate(enemy_Object, new Vector3(m_X, m_Y, m_Z), Quaternion.identity);

            realTime = 0;
        }
    }
}
