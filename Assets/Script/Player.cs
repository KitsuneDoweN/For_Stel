using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Hp = 100;
    public float speed = 10f;
    Rigidbody rb;

    public GameObject weapon_Main;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        InvokeRepeating("WeaponOnOff", 1f, 1.5f);
    }

    void FixedUpdate()
    {
        float zMove = Input.GetAxis("Horizontal");
        float xMove = Input.GetAxis("Vertical");

        Vector3 getVel = new Vector3(zMove, 0, xMove) * speed;
        rb.velocity = getVel;

        getVel = xMove* Vector3.forward + zMove * Vector3.right;

        this.transform.rotation = Quaternion.LookRotation(getVel);
        

    }

    void WeaponOnOff()
    {
        if(weapon_Main.activeSelf == true)
        {
            weapon_Main.SetActive(false);
            //Debug.Log("false");
        }
        else if(weapon_Main.activeSelf == false)
        {
            weapon_Main.SetActive(true);
            //Debug.Log("true");

        }

    }
}
