using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class Gun_Hero_1 : Controller
{
    [SerializeField]
    private Projectile[] projectile1 = new Projectile[] { };
    [SerializeField]
    private Projectile[] projectile2 = new Projectile[] { };
    [SerializeField]
    private GameObject sparkle;

    private bool skill1 = false;
    private bool skill2 = false;
    private bool skill3 = false;

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

        //shield
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

    }
    public void Skill3()
    {

    }
    private IEnumerator Casting_Skill1()
    {
        while(true)
        {
            ActiveProjectile(projectile2);

            yield return new WaitForSeconds(0.05f);
        }
    }
}