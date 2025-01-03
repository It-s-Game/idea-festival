using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class Gun_Hero_1 : Controller
{
    [SerializeField]
    private Projectile[] projectile1;
    [SerializeField]
    private Projectile[] projectile2;
    [SerializeField]
    private Projectile[] projectile3;
    [SerializeField]
    private GameObject shield_Range;
    [SerializeField]
    private GameObject sparkle;

    private CoolTime skill1 = new();
    private CoolTime skill2 = new();
    private CoolTime skill3 = new();

    protected override void Awake()
    {
        base.Awake();

        shield_Range.gameObject.SetActive(false);
    }
    protected override void DefaultAttack()
    {
        sparkle.SetActive(true);

        ActiveProjectile(projectile1);
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
        StartCoroutine(Casting_Skill1());
    }
    public void Skill2()
    {
        StartCoroutine(Casting_Skill2());
    }
    public void Skill3()
    {
        ActiveProjectile(projectile3);
    }
    private IEnumerator Casting_Skill1()
    {
        foreach(Projectile projectile in projectile2)
        {
            ActiveProjectile(projectile);

            yield return new WaitForSeconds(0.02f);
        }
    }
    private IEnumerator Casting_Skill2()
    {
        shield_Range.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.775f);

        shield_Range.gameObject.SetActive(false);
    }
}