using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class UI_Game : MonoBehaviour
{

    #region timer

    TextMeshProUGUI timer;
    public string timerText
    {
        get { return timer.text; }
        set
        {
            timer.text = value;
        }
    }
    float timeValue;

    public static bool isTimeEnd;

    float TimeValue
    {
        get => timeValue;
        set
        {
            timeValue = Mathf.Clamp(value, 0, float.PositiveInfinity);
            timerText = $"{(int)TimeValue / 60}:{(int)TimeValue % 60}";

            if (TimeValue <= 0)
                isTimeEnd = true;
        }
    }

    private void TimerInit()
    {
        timer = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TimeValue = 5;
    }

    private void TimerUpdate()
    {
        TimeValue -= Time.fixedDeltaTime;
    }

    #endregion

    private void Awake()
    {
        TimerInit();
    }

    private void FixedUpdate()
    {
        TimerUpdate();

        if (Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick2Button7) ||
            Input.GetKeyDown(KeyCode.Joystick3Button7) || Input.GetKeyDown(KeyCode.Joystick4Button7) ||
            Input.GetKeyDown(KeyCode.Escape))//Pause ют╥б
            transform.GetChild(2).gameObject.SetActive(true);
    }
}
