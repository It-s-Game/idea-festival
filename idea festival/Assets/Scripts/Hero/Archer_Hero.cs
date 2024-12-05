using UnityEngine;
public class Archer_Hero : CharacterController
{
    [SerializeField]
    private Projectile[] arrows = new Projectile[8];

    protected override void Attack(int direction)
    {
        foreach(Projectile arrow in arrows)
        {
            if(!arrow.gameObject.activeSelf)
            {
                arrow.gameObject.SetActive(true);

                arrow.Set(direction, gameObject);

                break;
            }
        }
    }
}