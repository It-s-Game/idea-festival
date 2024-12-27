using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_EndGame : MonoBehaviour
{
    bool isEndGame;

    private void FixedUpdate()
    {
        if(!isEndGame && UI_InGame.isTimeEnd)
        {
            isEndGame = true;
            GetComponentInChildren<Animator>().Play("End Game");

            Invoke("GoResult", 3f);
        }
    }

    private void GoResult()
    {
        SceneManager.LoadScene("Result");
    }
}
