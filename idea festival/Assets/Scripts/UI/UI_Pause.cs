using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Pause : MonoBehaviour
{
    private void OnEnable()
    {
        transform.GetChild(3).GetComponent<Button>().Select();
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void LeaveButton()
    {
        SceneManager.LoadScene("Main");
    }
}
