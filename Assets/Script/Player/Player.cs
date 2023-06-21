using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;

    private float AttackTime = 1.0f;
    private float realTime = 0f;
    private float offAttackTime = 0.75f;

    Rigidbody rb;

    //���� ����(AttackRange)
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
        //�̵� �� ���� ����
        float zMove = Input.GetAxis("Horizontal");
        float xMove = Input.GetAxis("Vertical");

        Vector3 getVel = new Vector3(zMove, 0, xMove) * speed;
        rb.velocity = getVel;

        getVel = xMove* Vector3.forward + zMove * Vector3.right;

        this.transform.rotation = Quaternion.LookRotation(getVel);
        

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
