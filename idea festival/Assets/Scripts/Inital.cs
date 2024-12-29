using UnityEngine;
public class Inital : MonoBehaviour
{
    private void Awake()
    {
        Managers.Game.GameStart();
    }
    private void Start()
    {
        int mapIndex = Random.Range(0, 3);

        GameObject go = Resources.Load<GameObject>("Map/Map_" + mapIndex);

        Instantiate(go);
    }
}