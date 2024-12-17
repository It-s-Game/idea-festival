using System.Collections;
using UnityEngine;
public class Projectile_2 : Projectile
{
    protected override void Move()
    {
        transform.position += direction * info.projectileSpeed * Time.deltaTime;
    }
    protected override IEnumerator Moving()
    {
        col.enabled = false;

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        col.enabled = true;

        animator.Play("flight");

        yield return base.Moving();
    }
}