using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Main : MonoBehaviour
{

    private GameObject setting;

    private void Start()
    {
        if (transform.parent.Find("Setting"))
            setting = transform.parent.Find("Setting").gameObject;
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Mode_Select");
    }

    public void SettingButton()
    {
        setting.SetActive(true);
    }

    public void LeaveButton()
    {
        Application.Quit(0);
    }
}
