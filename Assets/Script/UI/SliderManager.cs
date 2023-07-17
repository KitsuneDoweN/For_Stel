using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    Image StelExpBar;
    float maxExp;
    public static float exp;

    PlayerStatus playerStatus;

    private void Start()
    {
        playerStatus = GameObject.Find("Status_").GetComponent<PlayerStatus>();
        StelExpBar = GetComponent<Image>();
        
    }

    private void Update()
    {
        SliderExp();
    }

    void SliderExp()
    {
        exp = playerStatus.now_Player_Exp;
        maxExp = playerStatus.Max_Player_Exp;
        StelExpBar.fillAmount = exp / maxExp;
    }

}
