using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Camera : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //카메라 좌표와 동일하게 설정
        transform.position = player.transform.position + new Vector3(0, 38f, -32);
    }
}
