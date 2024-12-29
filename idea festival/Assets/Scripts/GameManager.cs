using System.Collections;
using System.Linq;
using UnityEngine;
public class GameManager
{
    public MapInfo mapInfo = null;

    public int[] spawnPointIndex;

    public void GameStart()
    {
        Managers.Instance.isInGame = true;

        Util.GetMonoBehaviour().StartCoroutine(GameSet());

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
    private IEnumerator GameSet()
    {
        yield return new WaitUntil(() => mapInfo != null);

        GameObject go;
        GameObject result;

        int playerCount = UI_CharacterSelect.playerCharacters.Count();

        spawnPointIndex = Util.GetRandomValues(4, playerCount);

        Debug.Log(playerCount);

        for (int i = 0; i < playerCount; i++)
        {
            go = Resources.Load<GameObject>("Hero/" + UI_CharacterSelect.playerCharacters[i].heroName);

            result = Object.Instantiate(go);

            Managers.Instance.players[i].Init(result.GetComponentInChildren<Controller>());
        }

        Util.GetMonoBehaviour().StartCoroutine(StartCountdown());
    }
    private IEnumerator StartCountdown()
    {
        yield return null;
    }
}