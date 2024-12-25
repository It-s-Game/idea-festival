using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_CharacterSelect : MonoBehaviour
{
    int playerCnt;
    int temp;

    public string[] playerCharacters;
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

    public void character(int i)
    {

        Transform curCharacter = transform.GetChild((int)UI_ModeSelect.mode).GetChild(temp).GetChild(1);
        curCharacter.GetComponent<Image>().sprite = unit.heroes[i].iconSprite;
        if (temp % 2 != 0)
            curCharacter.localScale = new Vector3(-1, 1, 1);
        playerCharacters[temp++] = unit.heroes[i].name;

        if (temp < playerCnt)
            return;
        SceneManager.LoadScene("Game");
    }
}
