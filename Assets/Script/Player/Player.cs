using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10;

    //���� �ð�
    private float AttackTime = 1.0f;
    //���� �ð�
    private float realTime = 0f;
    //���� �� �ٽ� ������ ������ �ð�
    private float offAttackTime = 0.75f;

    public Animator animator;

    Rigidbody rb;

    Vector3 look;

    //���� ����(AttackRange)
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

        //�̵� �� ���� ����
        float xMove = Input.GetAxis("Vertical");
        float zMove = Input.GetAxis("Horizontal");

        if (xMove != 0 || zMove != 0)
        {
            look = new Vector3(zMove, 0, xMove);
            transform.rotation = Quaternion.LookRotation(look);
            Vector3 getVel = transform.forward.normalized * speed;
            getVel.y = rb.velocity.y;
            rb.velocity = getVel;
            //animator.SetBool("Test", true);
        }
    }

    void WeaponOnOff()
    {
        //realTime�� �귯���� �ð� ����
        realTime += Time.deltaTime;

        //realTime�� ���� �ð����� ���ų� Ŭ �� && weapon_Main�� ���� ������ �۵�
        if (realTime >= AttackTime && weapon_Main.activeSelf == true)
        {
            //weapon_Main�� ����  realTime �ʱ�ȭ 
            weapon_Main.SetActive(false);
            realTime = 0;
        }
        //realTime�� ���� ���ð� ���� ���ų� Ŭ �� && AttackRange�� ���� ������ �۵�
        else if (realTime >= offAttackTime && weapon_Main.activeSelf == false)
        {
            //weapon_Main�� Ű��  realTime �ʱ�ȭ 
            weapon_Main.SetActive(true);
            realTime = 0;
        }
        
    }
}
