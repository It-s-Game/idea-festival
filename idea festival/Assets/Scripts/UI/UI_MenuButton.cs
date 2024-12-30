using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MenuButton : MonoBehaviour
{
    public void MainSceneButton()
    {
        SceneManager.LoadScene("Main");
    }

}
