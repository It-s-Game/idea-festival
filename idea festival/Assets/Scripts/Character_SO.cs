using UnityEngine;
[System.Serializable]
public class Status
{
    public float dash_coolTime;
    public int maxHealth;
    public int damage;
    public int moveSpeed;
    public int jumpHeight = 5;
}
[System.Serializable]
public class Attack
{
    public float coolTime;
    public float delay;
    public int damage;
    public bool isCancelable = true;
}
[CreateAssetMenu(fileName = "Character_SO", menuName = "Create New ScriptableObject/Character_SO")]
public class Character_SO : ScriptableObject
{
    public Attack[] skills = new Attack[] { };

    public Status status;
    public Attack default_Attack;
}