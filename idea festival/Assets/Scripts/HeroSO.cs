using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hero", menuName = "new Hero")]
[System.Serializable]
public class HeroSO : ScriptableObject
{
    public Sprite iconSprite;
    public string heroName {
        get => name;
    }

    public GameObject heroPrefab;
}
