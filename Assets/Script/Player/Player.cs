using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 0;

    //공격 시간
    private float AttackTime = 1.0f;
    //현재 시간
    private float realTime = 0f;
    //공격 후 다시 켜지기 전까지 시간
    private float offAttackTime = 0.75f;

    public Animator animator;

    Rigidbody rb;

    Vector3 look;

    //메인 무기(AttackRange)
    public GameObject weapon_Main;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        WeaponOnOff();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        speed = 10;

        //이동 및 방향 조정
        float xMove = Input.GetAxis("Vertical");
        float zMove = Input.GetAxis("Horizontal");

        if (xMove != 0 || zMove != 0)
        {
            look = new Vector3(zMove, 0, xMove);
            transform.rotation = Quaternion.LookRotation(look);
            Vector3 getVel = transform.forward.normalized * speed;
            getVel.y = rb.velocity.y;
            rb.velocity = getVel;
            speed = 10;
            //animator.SetBool("Test", true);
        }
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
