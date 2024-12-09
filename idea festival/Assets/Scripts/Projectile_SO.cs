using UnityEngine;
[System.Serializable]
public class Projectile_Info
{
    public Sprite defaultImage = null;

    public string defaultAnimationName = "";
    public string hitAnimationName = "";
    public float cooltime;
    public int damage;
    public int projectileSpeed;
}
[CreateAssetMenu(fileName = "Projectile Information", menuName = "Create New ScriptableObject/Projectile Information_SO")]
public class Projectile_SO : ScriptableObject
{
    public Projectile_Info info;
}