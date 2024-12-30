using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_CharacterSelect : MonoBehaviour
{
    int temp;

    public Sprite questionMark;

    static public HeroSO[] playerCharacters = new HeroSO[4];
    static public int playerCnt;

    static public void InitPlayerInfo()
    {
        playerCharacters = new HeroSO[4];
        playerCnt = 0;
    }

    public UnitSO unit;

    void Start()
    {
        unit = Resources.Load("Unit") as UnitSO;

        playerCnt = UI_ModeSelect.mode == 0 ? 2 : 4;
        if (playerCnt == 2)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Joystick2Button2) ||
            Input.GetKeyDown(KeyCode.Joystick3Button2) || Input.GetKeyDown(KeyCode.Joystick4Button2) ||
            Input.GetKeyDown(KeyCode.Backspace))//Cancel ют╥б
        {
            if (temp <= 0)
                return;

            playerCharacters[--temp] = null;
            Transform curCharacter = transform.GetChild((int)UI_ModeSelect.mode).GetChild(temp).GetChild(1);
            curCharacter.GetComponent<Image>().sprite = questionMark;
            if (temp % 2 != 0)
                curCharacter.localScale = new Vector3(1, 1, 1);

            if (temp >= playerCnt-1)
                transform.GetChild(3).gameObject.SetActive(false);

            transform.GetChild(2).GetChild(0).GetComponent<Button>().Select();
        }

    }

    public void character(int i)
    {
        Transform curCharacter = transform.GetChild((int)UI_ModeSelect.mode).GetChild(temp).GetChild(1);
        Managers.Game.icons.Add(unit.heroes[i].iconSprite);
        curCharacter.GetComponent<Image>().sprite = unit.heroes[i].iconSprite;
        if (temp % 2 != 0)
            curCharacter.localScale = new Vector3(-1, 1, 1);
        playerCharacters[temp++] = unit.heroes[i];

        if (temp < playerCnt)
            return;
        transform.GetChild(3).gameObject.SetActive(true);
        transform.GetChild(3).GetComponent<Button>().Select();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
