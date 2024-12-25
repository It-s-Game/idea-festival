using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Setting : MonoBehaviour
{
    public void OnEnable()
    {
        Slider slider1 = transform.GetChild(1).GetChild(1).GetComponent<Slider>();
        Slider slider2 = transform.GetChild(1).GetChild(2).GetComponent<Slider>();

        if (PlayerPrefs.HasKey("BGM"))
            slider1.value = PlayerPrefs.GetFloat("BGM");
        if (PlayerPrefs.HasKey("FBX"))
            slider2.value = PlayerPrefs.GetFloat("FBX");

        slider1.Select();
    }

    public void CloseSetting()
    {
        gameObject.SetActive(false);

        Slider slider1 = transform.GetChild(1).GetChild(1).GetComponent<Slider>();
        Slider slider2 = transform.GetChild(1).GetChild(2).GetComponent<Slider>();

        PlayerPrefs.SetFloat("BGM", slider1.value);
        PlayerPrefs.SetFloat("FBX", slider2.value);

        if (transform.parent.GetChild(0).GetChild(1).TryGetComponent(out Button button1))
            button1.Select();
        else if (transform.parent.GetChild(0).GetChild(2).GetChild(4).TryGetComponent(out Button button2))
            button2.Select();
    }
}
