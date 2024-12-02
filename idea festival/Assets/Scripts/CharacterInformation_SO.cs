using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Stat
{
    public int maxHealth;
    public int damage;
    public int attackSpeed;
    public int moveSpeed;
    public int jumpHeight = 5;
}
[CreateAssetMenu(fileName = "Player Information", menuName = "Create New ScriptableObject/Player Information_SO")]
public class CharacterInformation_SO : ScriptableObject
{
    public List<SkillInformation_SO> skillList = new();

    public Stat stat;
}