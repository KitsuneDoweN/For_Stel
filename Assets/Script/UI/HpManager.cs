using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
    Image HealthExpBar;
    float maxhealth;
    public static float health;

    PlayerStatus playerStatus;

    private void Start()
    {
        playerStatus = GameObject.Find("Status_").GetComponent<PlayerStatus>();
        HealthExpBar = GetComponent<Image>();

    }

    private void Update()
    {
        Health();
    }

    void Health()
    {
        health = playerStatus.Hp;
        maxhealth = playerStatus.MaxHp;
        HealthExpBar.fillAmount = health / maxhealth;
    }
}
