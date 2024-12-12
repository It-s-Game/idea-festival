using System.Collections;
using UnityEngine;
public class Archer_Hero_Dog : Projectile
{
    protected override void Move()
    {
        transform.position += direction * info.projectileSpeed * Time.deltaTime;
    }
    public override void Set(int direction, GameObject obj)
    {
        if(direction == 1)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        base.Set(direction, obj);
    }
    protected override IEnumerator Collide()
    {
        StopCoroutine(move);

        return base.Collide();
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