using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class UISupporter : MonoBehaviour
{
    enum Mode
    {
        TwoPlayer = 0,
        FourPlayer = 1
    }

    static Mode mode;
    
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject setting;

    static string preSceneName;


    private void Awake()
    {
        canvas = GameObject.Find("Canvas");

        if (canvas.transform.Find("Setting"))
            setting = canvas.transform.Find("Setting").gameObject;

        if (canvas.transform.Find("Character Select"))
            canvas.transform.Find("Character Select").GetChild((int)mode).gameObject.SetActive(true);

    }
    #region 공용

    public void OpenSetting()
    {
        setting.SetActive(true);
    }

    public void PreSceneButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }

    #endregion

    #region 메인화면

    public void StartButton()
    {
        SceneManager.LoadScene("Mode_Select");
    }

    public void LeaveButton()
    {
        Application.Quit(0);
    }

    #endregion

    #region 모드선택화면

    public void ModeSelect(int i)
    {
        mode = (Mode)i;
        SceneManager.LoadScene("Character_Select");
    }

    #endregion


    private void SceneLoad(string name)
    {
        preSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(name);
    }
}
