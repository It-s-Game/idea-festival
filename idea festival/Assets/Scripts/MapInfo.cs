using UnityEngine;
public class MapInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnPoints = new GameObject[4];

    public GameObject[] SpawnPoints { get { return spawnPoints; } }
    private void Awake()
    {
        Managers.Game.mapInfo = this;
    }
}