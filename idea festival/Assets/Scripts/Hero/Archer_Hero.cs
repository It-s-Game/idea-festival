using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class Archer_Hero : Controller
{
    [SerializeField]
    private Projectile[] projectile1 = new Projectile[] { };
    [SerializeField]
    private Projectile[] projectile2 = new Projectile[] { };
    [SerializeField]
    private AttackRange skill2_Range;

    private bool skill1 = false;
    private bool skill2 = false;
    private bool skill3 = false;

    protected override void DefaultAttack()
    {
        ActiveProjectile(projectile1);
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
        if(skill3)
        {
            return;
        }

        StartCoroutine(Casting_Skill3());
        StartCoroutine(Dash("skill3", 2.5f));
    }
    public void Skill1()
    {
        ActiveProjectile(projectile2);
    }
    public void Skill2()
    {
        StartCoroutine(Casting_Skill2());
    }
    private IEnumerator Casting_Skill2()
    {
        skill2_Range.gameObject.SetActive(true);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        skill2_Range.gameObject.SetActive(false);
    }
    private IEnumerator Casting_Skill3()
    {
        skill3 = true;

        yield return new WaitForSeconds(so.skills[2].coolTime);
        
        skill3 = false;
    }
}