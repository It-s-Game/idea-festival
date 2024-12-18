using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class Cowboy_Hero : Controller
{
    [SerializeField]
    private Projectile[] defaultAttacks = new Projectile[] { };
    [SerializeField]
    private AttackRange skill2_Range;

    private bool skill1 = false;
    private bool skill2 = false;
    private bool skill3 = false;
    private bool skill4 = false;
    private bool skill5 = false;

    protected override void DefaultAttack()
    {
        StartCoroutine(Casting_DefaultAttack());
    }
    protected override void Awake()
    {
        base.Awake();

        skill2_Range.Init(gameObject, so.skills[1].damage);
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
    public override void LeftTrigger(InputValue value)
    {
        if(isJump)
        {
            return;
        }

        Skill(Skill4, "skill4", so.skills[3].delay, ref skill4);
    }
    public override void RightTrigger(InputValue value)
    {
        if(isJump)
        {
            return;
        }

        Skill(Skill1, "skill5", so.skills[4].delay, ref skill5);
    }
    public void Skill1()
    {

    }
    public void Skill2()
    {
        StartCoroutine(Casting_Skill2());
    }
    public void Skill3()
    {

    }
    public void Skill4()
    {

    }
    public void Skill5()
    {

    }
    private IEnumerator Casting_DefaultAttack()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f);

        ActiveProjectile(defaultAttacks);
        
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f);

        ActiveProjectile(defaultAttacks);
    }
    private IEnumerator Casting_Skill2()
    {
        skill2_Range.gameObject.SetActive(true);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        skill2_Range.gameObject.SetActive(false);
    }
}