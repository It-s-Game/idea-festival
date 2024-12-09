using UnityEngine;
[System.Serializable]
public class Status
{
    public float[] skills = new float[] { };

    public float defaultAttack;
    public int maxHealth;
    public int damage;
    public int attackDelay;
    public int moveSpeed;
    public int jumpHeight = 5;
    public bool jumpAttack = false;
}
[CreateAssetMenu(fileName = "Player Information", menuName = "Create New ScriptableObject/Player Information_SO")]
public class CharacterInformation_SO : ScriptableObject
{
    public Status status;
}