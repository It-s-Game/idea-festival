using System.Collections;
using System.Collections.Generic;
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
        transform.GetChild(1).GetComponent<Button>().Select();

        transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = winnerSprite;
        transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Text>().text = $"{winnerNum}P" ;
    }
}
