using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class Bat_Hero : Controller
{
    [SerializeField]
    private Projectile[] projectile1 = new Projectile[] { };
    [SerializeField]
    private Projectile[] projectile2 = new Projectile[] { };
    [SerializeField]
    private AttackRange defaultAttack_Range;
    [SerializeField]
    private AttackRange skill3_Range;

    private bool skill1 = false;
    private bool skill2 = false;
    private bool skill3 = false;

    protected override void DefaultAttack()
    {
        StartCoroutine(Casting_DefaultAttack());
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
        if(isJump)
        {
            return;
        }

        Skill(Skill3, "skill3", so.skills[2].delay, ref skill3);
    }
    public void Skill1()
    {
        ActiveProjectile(projectile1);
    }
    public void Skill2()
    {
        ActiveProjectile(projectile2);
    }
    public void Skill3()
    {
        StartCoroutine(Casting_Skill3());
    }
    private IEnumerator Casting_DefaultAttack()
    {
        defaultAttack_Range.gameObject.SetActive(true);

        defaultAttack_Range.Set(so.default_Attack.damage);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        defaultAttack_Range.gameObject.SetActive(false);
    }
    private IEnumerator Casting_Skill3()
    {
        skill3_Range.gameObject.SetActive(true);

        skill3_Range.Set(so.skills[1].damage);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        //move charcter transform position;

        skill3_Range.gameObject.SetActive(false);
    }
}