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

    private bool skill1 = false;
    private bool skill2 = false;
    private bool skill3 = false;

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