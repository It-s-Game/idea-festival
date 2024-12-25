using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_ModeSelect : MonoBehaviour
{
    public enum Mode
    {
        TwoPlayer = 0,
        FourPlayer = 1
    }

    public static Mode mode;

    public void ModeSelect(int i)
    {
        mode = (Mode)i;
        SceneManager.LoadScene("Character_Select");
    }
}
