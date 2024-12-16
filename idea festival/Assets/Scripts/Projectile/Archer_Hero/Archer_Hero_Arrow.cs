using System.Collections;
using UnityEngine;
public class Archer_Hero_Arrow : Projectile
{
    public override void Set(int direction, GameObject obj)
    {
        base.Set(direction, obj);

        animator.Play(info.defaultAnimationName);
    }
    protected override void Move()
    {
        transform.position += direction * info.projectileSpeed * Time.deltaTime;
    }
    protected override IEnumerator Collide()
    {
        StopCoroutine(move);

        return base.Collide();
    }
}