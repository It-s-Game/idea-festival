using UnityEngine;
using UnityEngine.InputSystem;
public class Gun_Hero_3 : Controller
{
    [SerializeField]
    private Projectile[] projectile1 = new Projectile[] { };
    [SerializeField]
    private Projectile[] projectile2 = new Projectile[] { };

    private bool skill1 = false;
    private bool skill2 = false;
    private bool skill3 = false;
    private bool skill4 = false;

    protected override void DefaultAttack()
    {
        ActiveProjectile(projectile1);
    }
    public override void ButtonY(InputValue value)
    {
        if(isJump)
        {
            return;
        }

        Skill(Skill1, "skill1", so.skills[0], ref skill1);
    }
    public override void ButtonB(InputValue value)
    {
        if(isJump)
        {
            return;
        }

        Skill(Skill2, "sklll2", so.skills[1], ref skill2);
    }
    public override void RightBumper(InputValue value)
    {
        
    }
    public override void LeftTrigger(InputValue value)
    {
        
    }
    public void Skill1()
    {

    }
    public void Skill2()
    {

    }
    public void Skill3()
    {

    }
    public void Skill4()
    {
        
    }
}