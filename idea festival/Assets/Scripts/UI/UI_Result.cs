using UnityEngine;
using UnityEngine.UI;
public class UI_Result : MonoBehaviour
{
    public static string winnerNum;
    public static Sprite winnerSprite;

    public string winNum;
    public Sprite winSprite;
    private void OnEnable()
    {
        transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = Managers.Game.icons[Managers.Game.winnerIndex];
        transform.GetChild(1).GetChild(2).GetComponent<Text>().text = $"{Managers.Game.winnerIndex + 1}P";
    }
}