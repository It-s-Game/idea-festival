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
    private AttackRange skill3_Range;

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
        skill3_Range.Init(gameObject, so.skills[0].damage);
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
        StartCoroutine(Dash("skill3", so.skills[2].coolTime, 1.6f, skill3_Range.gameObject, skill3));
    }
    public void Skill1()
    {
        StartCoroutine(Casting_Skill1());
    }
    public void Skill2()
    {
        ActiveProjectile(skill2_Projectile);
    }
    private IEnumerator Casting_Skill1()
    {
        stamina = 0;

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        stamina = 1;
    }
    private IEnumerator Casting_DefaultAttack()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.25f);

        defaultAttack_Range.gameObject.SetActive(true);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.625f);

        defaultAttack_Range.gameObject.SetActive(false);
    }
}