using UnityEngine;
using UnityEngine.InputSystem;
public class Archer_Hero : Controller
{
    [SerializeField]
    private Projectile[] arrows = new Projectile[] { };

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
    public override void ButtonY(InputValue value)
    {

    }
}