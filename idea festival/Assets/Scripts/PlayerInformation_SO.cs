using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Information", menuName = "Create New ScriptableObject/Player Information_SO")]
public class PlayerInformation_SO : ScriptableObject
{
    public List<SkillInformation_SO> skillList = new();

    public float health;
    public float damage;
    public float attackSpeed;
    public float moveSpeed;
}