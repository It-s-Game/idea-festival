using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class Big_Sword_Hero_3 : Controller
{
    [SerializeField]
    private Projectile[] projectile1 = new Projectile[] { };
    [SerializeField]
    private AttackRange defaultAttack_Range;
    [SerializeField]
    private Shield shield;

    private bool skill1 = false;
    private bool skill2 = false;
    private bool skill3 = false;

    protected override void DefaultAttack()
    {
        StartCoroutine(Casting_DefaultAttack());
    }
    protected override void Awake()
    {
        base.Awake();

        defaultAttack_Range.Init(gameObject, so.default_Attack.damage);
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

        StartCoroutine(Casting_Skill2());
        Skill(Skill2, "skill2", so.skills[1], ref skill2);
    }
    public override void RightBumper(InputValue value)
    {
        if(isJump)
        {
            return;
        }

        shield.gameObject.SetActive(true);
        shield.Set(direction);

        StartCoroutine(Casting_Skill3());
    }
    public void Skill1()
    {
        StartCoroutine(Casting_Skill1());
    }
    public void Skill2()
    {
        rigid.velocity = new Vector3(direction * status.moveSpeed * 1.2f, 0);
    }
    public void Skill3()
    {
        shield.gameObject.SetActive(true);
        shield.Set(direction);
    }
    private IEnumerator Casting_DefaultAttack()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f);

        defaultAttack_Range.gameObject.SetActive(true);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        defaultAttack_Range.gameObject.SetActive(false);
    }
    private IEnumerator Casting_Skill1()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.45f);

        ActiveProjectile(projectile1);
    }
    private IEnumerator Casting_Skill2()
    {
        skill2 = true;

        yield return new WaitForSeconds(so.skills[1].coolTime);
        
        skill2 = false;
    }
    private IEnumerator Casting_Skill3()
    {
        skill3 = true;

        yield return new WaitForSeconds(so.skills[2].coolTime);

        skill3 = false;
    }
}