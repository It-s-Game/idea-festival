using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager
{
    public List<Sprite> icons = new();
    public MapInfo mapInfo = null;
    public UI_InGame inGame;

    public int[] spawnPointIndex;
    public int playerCount = 0;
    public int winnerIndex;

    private int numberOfSurvivors = 0;

    public int NumberOfSurvivors
    {
        get
        {
            return numberOfSurvivors;
        }
        set 
        {
            numberOfSurvivors = value;

            Debug.Log(NumberOfSurvivors);

            if(numberOfSurvivors == 1)
            {
                GameOver();
            }
        }
    }
    public void GameStart()
    {
        Util.GetMonoBehaviour().StartCoroutine(GameSet());
    }
    public void GameOver()
    {
        int maxPoint = 0;

        foreach(Player player in Managers.Instance.players)
        {
            int point = 0;

            point += (int)player.controller.Health;
            point += player.controller.life * 10;

            if(point >= maxPoint)
            {
                maxPoint = point;

                winnerIndex = player.PlayerIndex;
            }

            player.Resetting();
        }

        for(int i = 0; i < Managers.Instance.players.Count; i++)
        {
            Managers.Instance.players[i].Resetting();
        }

        numberOfSurvivors = 0;

        SceneManager.LoadScene("Result");
    }
    private IEnumerator GameSet()
    {
        yield return new WaitUntil(() => mapInfo != null);

        yield return new WaitUntil(() => inGame != null);

        GameObject go;
        GameObject character;

        spawnPointIndex = Util.GetRandomValues(4, playerCount);

        for (int i = 0; i < playerCount; i++)
        {
            go = Resources.Load<GameObject>("Hero/" + UI_CharacterSelect.playerCharacters[i].heroName);

            character = Object.Instantiate(go);

            Managers.Instance.players[i].Init(character.GetComponentInChildren<Controller>());

            inGame.characters[i] = character.GetComponentInChildren<Character>();

            numberOfSurvivors++;
        }

        yield return null;

        Util.GetMonoBehaviour().StartCoroutine(inGame.PlayerInfoUpdate());
    }
}