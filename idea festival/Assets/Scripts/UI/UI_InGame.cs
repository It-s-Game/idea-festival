using System.Collections;
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

    private Slider[] sliders = new Slider[4];
    private GameObject[] lifes = new GameObject[4];

    private void Awake()
    {
        Managers.Game.inGame = this;

        TimerInit();
        InitPlayerInfo();
    }

    private void FixedUpdate()
    {
        TimerUpdate();

        if (Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick2Button7) ||
            Input.GetKeyDown(KeyCode.Joystick3Button7) || Input.GetKeyDown(KeyCode.Joystick4Button7) ||
            Input.GetKeyDown(KeyCode.Escape))
            transform.GetChild(2).gameObject.SetActive(true);


    }

    private void InitPlayerInfo()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < Managers.Game.playerCount)
            {
                playerInfo[i].SetActive(true);

                Image icon = playerInfo[i].transform.GetChild(0).GetComponent<Image>();

                icon.sprite = Managers.Game.icons[i];
            }
            else
            {
                playerInfo[i].SetActive(false);
            }

            sliders[i] = playerInfo[i].transform.GetChild(1).GetComponent<Slider>();
            lifes[i] = playerInfo[i].transform.GetChild(2).gameObject;
        }
    }

    private void PlayerHealthUpdate()
    {
        for (int i = 0; i < Managers.Game.playerCount; i++)
        {
            sliders[i].value = characters[i].Health / characters[i].MaxHealth;
        }
    }
    public void PlayerLifeUpdate(int playerIndex, int reamainingLife)
    {
        lifes[playerIndex].transform.GetChild(reamainingLife).gameObject.SetActive(false);
    }
    public IEnumerator PlayerInfoUpdate()
    {
        while(true)
        {
            PlayerHealthUpdate();

            yield return null;
        }
    }
}
