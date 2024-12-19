using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class Cowboy_Hero : Controller
{
    [SerializeField]
    private Projectile[] defaultAttacks = new Projectile[] { };
    [SerializeField]
    private Projectile[] skill1_Projectile = new Projectile[] { };
    [SerializeField]
    private Projectile[] skill3_Projectile = new Projectile[] { };
    [SerializeField]
    private AttackRange skill2_Range;
    [SerializeField]
    private AttackRange skill4_Range;

    private bool skill1 = false;
    private bool skill2 = false;
    private bool skill3 = false;
    private bool skill4 = false;

    protected override void DefaultAttack()
    {
        StartCoroutine(Casting_DefaultAttack());
    }
    protected override void Awake()
    {
        base.Awake();

        skill2_Range.Init(gameObject, so.skills[1].damage);
        skill4_Range.Init(gameObject, so.skills[3].damage);
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

        Skill(Skill2, "skill2", so.skills[1], ref skill2);
    }
    public override void RightBumper(InputValue value)
    {
        if(isJump)
        {
            return;
        }

        Skill(Skill3, "skill3", so.skills[2], ref skill3);
    }
    public override void LeftTrigger(InputValue value)
    {
        if(isJump)
        {
            return;
        }

        Skill(Skill4, "skill4", so.skills[3], ref skill4);
    }
    public void Skill1()
    {
        ActiveProjectile(skill1_Projectile);
    }
    public void Skill2()
    {
        StartCoroutine(Casting_Skill2());
    }
    public void Skill3()
    {
        ActiveProjectile(skill3_Projectile);
    }
    public void Skill4()
    {
        StartCoroutine(Casting_Skill4());
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
    private IEnumerator Casting_Skill4()
    {
        skill4_Range.gameObject.SetActive(true);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6);
        
        skill4_Range.gameObject.SetActive(false);
    }
}