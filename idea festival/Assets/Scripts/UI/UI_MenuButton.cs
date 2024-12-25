using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MenuButton : MonoBehaviour
{
    public void PreSceneButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void MainSceneButton()
    {
        SceneManager.LoadScene("Main");
    }

}
