using UnityEngine;
using UnityEngine.InputSystem;
public class Archer_Hero : Controller
{
    [SerializeField]
    private Projectile[] arrows = new Projectile[] { };
    [SerializeField]
    private Projectile[] dogs = new Projectile[] { };

    protected override void Attack()
    {
        ActiveProjectile(arrows);
    }
    public override void ButtonY(InputValue value)
    {
        ActiveProjectile(dogs);
    }
}