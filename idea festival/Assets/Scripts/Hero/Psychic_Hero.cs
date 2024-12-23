using UnityEngine;
using UnityEngine.InputSystem;
public class Psychic_Hero : Controller
{
    [SerializeField]
    private Projectile[] defaultAttack_Projectile;
    [SerializeField]
    private Projectile[] skill1_Projectile;
    [SerializeField]
    private Projectile[] skill2_Projectile;

    private bool skill1;
    private bool skill2;
    private bool skill3;

    protected override void DefaultAttack()
    {
        ActiveProjectile(defaultAttack_Projectile);
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
        if (isJump)
        {
            return;
        }

        Skill(Skill2, "skill2", so.skills[1], ref skill2);
    }
    public override void RightBumper(InputValue value)
    {
        if (isJump)
        {
            return;
        }

        Skill(Skill3, "skill3", so.skills[2], ref skill3);
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
}