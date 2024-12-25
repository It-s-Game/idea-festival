using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hero", menuName = "new Hero")]
[System.Serializable]
public class HeroSO : ScriptableObject
{
    public string heroName;
    public Sprite iconSprite;
}
