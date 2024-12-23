using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class Lancer_Hero : Controller
{
    [SerializeField]
    private AttackRange defaultAttack_Range;
    [SerializeField]
    private AttackRange skill1_Range;
    [SerializeField]
    private Projectile[] skill2_Projectile;
    [SerializeField]
    private Projectile[] skill3_Projectile;
    [SerializeField]
    private GameObject shield_Range;
    [SerializeField]
    private AttackRange skill5_Range;

    private bool skill1;
    private bool skill2;
    private bool skill3;
    private bool skill4;
    private bool skill5;

    protected override void DefaultAttack()
    {
        StartCoroutine(Casting_DefaultAttack());
    }
    protected override void Awake()
    {
        base.Awake();

        defaultAttack_Range.Init(gameObject, so.default_Attack.damage);
        skill1_Range.Init(gameObject, so.skills[0].damage);
        skill5_Range.Init(gameObject, so.skills[4].damage);
        shield_Range.SetActive(false);
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
    public override void LeftTrigger(InputValue value)
    {
        if (isJump)
        {
            return;
        }

        Skill(Skill4, "skill4", so.skills[3], ref skill4);
    }
    public override void RightTrigger(InputValue value)
    {
        if (isJump)
        {
            return;
        }

        Skill(Skill5, "skill5", so.skills[4], ref skill5);
    }
    public void Skill1()
    {
        StartCoroutine(Casting_Skill1());
    }
    public void Skill2()
    {
        ActiveProjectile(skill2_Projectile);
    }
    public void Skill3()
    {
        ActiveProjectile(skill3_Projectile);
    }
    public void Skill4()
    {
        StartCoroutine(Casting_Skill4());
    }
    public void Skill5()
    {
        StartCoroutine(Casting_Skill5());
    }
    private IEnumerator Casting_DefaultAttack()
    {
        defaultAttack_Range.gameObject.SetActive(true);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        defaultAttack_Range.gameObject.SetActive(false);
    }
    private IEnumerator Casting_Skill1()
    {
        skill1_Range.gameObject.SetActive(true);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        skill1_Range.gameObject.SetActive(false);
    }
    private IEnumerator Casting_Skill4()
    {
        shield_Range.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.7f);

        shield_Range.gameObject.SetActive(false);
    }
    private IEnumerator Casting_Skill5()
    {
        skill5_Range.gameObject.SetActive(true);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        skill5_Range.gameObject.SetActive(false);
    }
}