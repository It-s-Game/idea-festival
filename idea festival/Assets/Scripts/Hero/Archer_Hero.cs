using UnityEngine;
using UnityEngine.InputSystem;
public class Archer_Hero : Controller
{
    [SerializeField]
    private Projectile[] arrows = new Projectile[] { };
    [SerializeField]
    private Projectile[] dogs = new Projectile[] { };

    private bool skill1 = false;
    private bool skill2 = false;
    private bool skill3 = false;

    protected override void DefaultAttack()
    {
        ActiveProjectile(arrows);
    }
    public override void ButtonY(InputValue value)
    {
        if(isJump)
        {
            return;
        }

        Skill(Skill1, "skill1", so.skills[0].delay, ref skill1);
    }
    public override void ButtonB(InputValue value)
    {
        if(isJump)
        {
            return;
        }

        Skill(Skill2, "skill2", so.skills[1].delay, ref skill2);
    }
    public override void RightBumper(InputValue value)
    {
        Skill(Skill3, "skill3", so.skills[2].delay, ref skill3);
    }
    public void Skill1()
    {
        ActiveProjectile(dogs);
    }
    public void Skill2()
    {
        
    }
    public void Skill3()
    {

    }
}