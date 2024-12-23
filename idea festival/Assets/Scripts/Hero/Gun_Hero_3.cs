using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class Gun_Hero_3 : Controller
{
    [SerializeField]
    private Projectile[] projectile1;
    [SerializeField]
    private Projectile projectile2;
    [SerializeField]
    private Projectile[] projectile3;
    [SerializeField]
    private GameObject shield_Range;
    [SerializeField]
    private AttackRange skill4_Range;

    private bool skill1 = false;
    private bool skill2 = false;
    private bool skill3 = false;
    private bool skill4 = false;

    protected override void DefaultAttack()
    {
        ActiveProjectile(projectile1);
    }
    protected override void Awake()
    {
        base.Awake();

        skill4_Range.Init(gameObject, so.skills[3].damage);
    }
    public override void ButtonY(InputValue value)
    {
        Skill(Skill1, "skill1", so.skills[0], ref skill1);
    }
    public override void ButtonB(InputValue value)
    {
        Skill(Skill2, "skill2", so.skills[1], ref skill2);
    }
    public override void RightBumper(InputValue value)
    {
        Skill(Skill3, "skill3", so.skills[2], ref skill3);
    }
    public override void LeftTrigger(InputValue value)
    {
        if(skill4)
        {
            return;
        }

        StartCoroutine(Casting_Skill4());
        StartCoroutine(Dash("skill4", 1.35f, skill4_Range.gameObject));
    }
    public void Skill1()
    {
        ActiveProjectile(projectile2);
    }
    public void Skill2()
    {
        StartCoroutine(Casting_Skill2());
    }
    public void Skill3()
    {
        ActiveProjectile(projectile3);
    }
    private IEnumerator Casting_Skill2()
    {
        shield_Range.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.65f);

        shield_Range.gameObject.SetActive(false);
    }
    private IEnumerator Casting_Skill4()
    {
        skill4 = true;

        yield return new WaitForSeconds(so.skills[3].coolTime);

        skill4 = false;
    }
}