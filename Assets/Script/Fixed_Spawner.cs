using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixed_Spawner : MonoBehaviour
{
    private float realTime;
    public GameObject enemy_Object;


    // Update is called once per frame
    void Update()
    {
        realTime += Time.deltaTime;

        if(realTime >= 5)
        {
            //스폰지역 범위
            float m_X = Random.Range(50, 50f);
            float m_Y = Random.Range(3, 3);
            float m_Z = Random.Range(50, 50f);

            Instantiate(enemy_Object, new Vector3(m_X, m_Y, m_Z), Quaternion.identity);

            realTime = 0;
        }
    }
}
