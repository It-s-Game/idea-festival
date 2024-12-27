using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{

    #region timer
    public float defaultTime;


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
        TimeValue = defaultTime;
    }

    private void TimerUpdate()
    {
        TimeValue -= Time.fixedDeltaTime;
    }

    #endregion

    public Vector3[] spawnPoint;

    public GameObject[] playerInfo;
    public Character[] characters = new Character[4];

    private void Awake()
    {
        TimerInit();
        InitPlayerInfo();
    }

    private void FixedUpdate()
    {
        TimerUpdate();
        playerInfoUpdate();

        if (Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick2Button7) ||
            Input.GetKeyDown(KeyCode.Joystick3Button7) || Input.GetKeyDown(KeyCode.Joystick4Button7) ||
            Input.GetKeyDown(KeyCode.Escape))
            transform.GetChild(2).gameObject.SetActive(true);


    }

    private void InitPlayerInfo()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < UI_CharacterSelect.playerCnt)
                playerInfo[i].SetActive(true);
            else
                playerInfo[i].SetActive(false);
        }

        for (int i = 0; i < UI_CharacterSelect.playerCnt; i++)
        {
            characters[i] = Instantiate(UI_CharacterSelect.playerCharacters[i].heroPrefab, spawnPoint[i], Quaternion.identity).
                transform.GetChild(0).GetComponent<Character>();
        }
    }

    private void playerInfoUpdate()
    {
        for (int i = 0; i < UI_CharacterSelect.playerCnt; i++)
        {
            playerInfo[i].transform.GetChild(1).GetComponent<Slider>().value = characters[i].Health / characters[i].MaxHealth;
        }
    }
}
