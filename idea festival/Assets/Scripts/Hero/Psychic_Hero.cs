using System.Collections;
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
    [SerializeField]
    private AttackRange skill3_Range;

    private CoolTime skill1 = new();
    private CoolTime skill2 = new();
    private CoolTime skill3 = new();

    protected override void DefaultAttack()
    {
        ActiveProjectile(defaultAttack_Projectile);
    }
    protected override void Awake()
    {
        base.Awake();

        skill3_Range.Init(gameObject, so.skills[2].damage);
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
        Skill(Skill3, "skill3", so.skills[2], skill3);
    }
    public void Skill1()
    {
        ActiveProjectile(skill1_Projectile);
    }
    public void Skill2()
    {
        skill3_Range.gameObject.SetActive(true);
        ActiveProjectile(skill2_Projectile);
    }
    public void Skill3()
    {
        rigid.velocity = new Vector3(direction * status.moveSpeed * 1.2f, 0);

        StartCoroutine(Casting_Skill3());
    }
    private IEnumerator Casting_Skill3()
    {
        skill3_Range.gameObject.SetActive(true);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        skill3_Range.gameObject.SetActive(false);

        yield return new WaitForSeconds(so.skills[2].coolTime);
    }
}