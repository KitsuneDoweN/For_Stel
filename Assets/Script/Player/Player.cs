using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;

    //공격 시간
    private float AttackTime = 1.0f;
    //현재 시간
    private float realTime = 0f;
    //공격 후 다시 켜지기 전까지 시간
    private float offAttackTime = 0.75f;

    Rigidbody rb;

    //메인 무기(AttackRange)
    public GameObject weapon_Main;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        WeaponOnOff();
    }
    void FixedUpdate()
    {
        //이동 및 방향 조정
        float zMove = Input.GetAxis("Horizontal");
        float xMove = Input.GetAxis("Vertical");

        Vector3 getVel = new Vector3(zMove, 0, xMove) * speed;
        rb.velocity = getVel;

        getVel = xMove* Vector3.forward + zMove * Vector3.right;

        this.transform.rotation = Quaternion.LookRotation(getVel);
        

    }

    void WeaponOnOff()
    {
        //realTime에 흘러가는 시간 적용
        realTime += Time.deltaTime;

        //realTime이 공격 시간보다 같거나 클 때 && weapon_Main가 켜져 있으면 작동
        if (realTime >= AttackTime && weapon_Main.activeSelf == true)
        {
            //weapon_Main을 끄고  realTime 초기화 
            weapon_Main.SetActive(false);
            realTime = 0;
        }
        //realTime이 공격 대기시간 보다 같거나 클 때 && AttackRange가 꺼져 있으면 작동
        else if (realTime >= offAttackTime && weapon_Main.activeSelf == false)
        {
            //weapon_Main을 키고  realTime 초기화 
            weapon_Main.SetActive(true);
            realTime = 0;
        }
        
    }

}
