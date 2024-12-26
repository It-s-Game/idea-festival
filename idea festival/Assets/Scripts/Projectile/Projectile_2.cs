using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class Projectile_2 : Projectile
{
    protected override IEnumerator Moving()
    {
        col.enabled = false;

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        col.enabled = true;

        animator.Play("flight");

        yield return base.Moving();
    }
}