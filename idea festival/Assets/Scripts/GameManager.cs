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

            if(Managers.Instance.isInGame && numberOfSurvivors == 1)
            {
                GameOver();
            }
        }
    }
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

        icons = new();
        numberOfSurvivors = 0;
        Managers.Instance.isInGame = false;

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
        }

        yield return null;

        Util.GetMonoBehaviour().StartCoroutine(inGame.PlayerInfoUpdate());
    }
}