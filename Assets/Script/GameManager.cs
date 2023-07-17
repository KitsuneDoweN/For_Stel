using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text[] timeText;
    public TMP_Text LevelCount;
    private float timeCount = 00;
    int min, sec;

    PlayerStatus playerStatus;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = GameObject.Find("Status_").GetComponent<PlayerStatus>();

        timeText[0].text = "00";
        timeText[1].text = "00";

    }

    void Update()
    {
        StopWatch();
        LevelUpManager();
    }

    void StopWatch()
    {
        timeCount += Time.deltaTime;
        min = (int)timeCount / 60;
        sec = ((int)timeCount - min * 60) % 60;

        if (min <= 0 && sec <= 0)
        {
            timeText[0].text = 0.ToString();
            timeText[1].text = 0.ToString();
        }
        else
        {
            if (sec >= 60)
            {
                min += 1;
                sec -= 60;
            }
            else
            {
                timeText[0].text = min.ToString();
                timeText[1].text = sec.ToString();
            }
        }
    }

    void LevelUpManager()
    {
        LevelCount.text = playerStatus.Level.ToString();
    }
}
