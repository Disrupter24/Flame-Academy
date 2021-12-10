using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerCountup : MonoBehaviour
{
    public TMP_Text TimerText;
    private float TimePassed;
    private float TimeDisplay;
    void Update()
    {
        TimePassed += Time.deltaTime;
        TimeDisplay = (Mathf.Round(TimePassed * 10)) / 10;
        TimerText.text = (TimeDisplay + "s");
        if (Input.GetKeyDown("space")) // Resets Timer
        {
            TimePassed = 0;
        }
    }
}
