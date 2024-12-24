using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class Lightning_Hero : Controller
{
    [SerializeField]
    private AttackRange defaultAttack_Range;
    [SerializeField]
    private Projectile[] skill2_Projectile;
    [SerializeField]
    private AttackRange skill1_Range;

    private CoolTime skill1 = new();
    private CoolTime skill2 = new();
    private CoolTime skill3 = new();

    protected override void DefaultAttack()
    {
        StartCoroutine(Casting_DefaultAttack());
    }
    protected override void Awake()
    {
        base.Awake();

        defaultAttack_Range.Init(gameObject, so.default_Attack.damage);
        skill1_Range.Init(gameObject, so.skills[0].damage);
    }
    public override void ButtonY(InputValue value)
    {
        Skill(Skill1, "skill1", so.skills[0], skill1);
    }
    public override void ButtonB(InputValue value)
    {
        Skill(Skill2, "skill2", so.skills[1], skill2);
    }
    public override void RightBumper(InputValue value)
    {
        if(skill3.isInCoolTime)
        {
            return;
        }

        StartCoroutine(Casting_Skill3());
        StartCoroutine(Dash("skill3", 1.6f));
    }
    public void Skill1()
    {

    }
    public void Skill2()
    {
        ActiveProjectile(skill2_Projectile);
    }
    private IEnumerator Casting_DefaultAttack()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.25f);

        defaultAttack_Range.gameObject.SetActive(true);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.625f);

        defaultAttack_Range.gameObject.SetActive(false);
    }
    private IEnumerator Casting_Skill3()
    {
        skill3.isInCoolTime = true;

        yield return new WaitForSeconds(so.skills[2].coolTime);

        skill3.isInCoolTime = false;
    }
}