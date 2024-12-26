using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager
{
    public List<string> characterName = new(); 

    private void GameSet()
    {
        GameObject go;
        GameObject result;

        for (int i = 0; i < Managers.Instance.players.Count; i++)
        {
            go = Resources.Load<GameObject>("Hero/" + characterName[i]);

            result = Object.Instantiate(go);

            Managers.Instance.players[i].Init(result.GetComponentInChildren<Controller>(), Vector3.zero);
        }
    }
    public void GameStart()
    {
        Managers.Instance.isInGame = true;

        Util.GetMonoBehaviour().StartCoroutine(StartCountdown());

        GameSet();

        //Managers.UI.ActiveUI("Player_NameTag");
    }
    public void GameOver()
    {
        foreach(Player player in Managers.Instance.players)
        {
            player.Resetting();
        }

        Managers.Instance.isInGame = false;
    }
    private IEnumerator StartCountdown()
    {
        yield return null;
    }
}