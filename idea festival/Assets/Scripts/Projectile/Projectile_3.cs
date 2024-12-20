using System.Collections;
using UnityEngine;
public class Projectile_3 : Projectile
{
    private Rigidbody2D rigid = null;

    private bool isExplosion = false;

    protected override void OnEnable()
    {
        base.OnEnable();

        isExplosion = false;
    }
    private void Start()
    {
        rigid = gameObject.AddComponent<Rigidbody2D>();
    }
    protected override void Move()
    {
        rigid.velocity = new Vector3(direction.x * 2, 5);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == obj)
        {
            return;
        }

        if(!isExplosion)
        {
            if (collision.gameObject.CompareTag("floor"))
            {
                StartCoroutine(EnterCollide());
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == obj)
        {
            return;
        }

        if (isExplosion)
        {
            if (collision.gameObject.TryGetComponent(out IDamagable damagable))
            {
                foreach (GameObject go in objects)
                {
                    if (collision.gameObject == go)
                    {
                        return;
                    }
                }

                damagable.TakeDamage(info.damage);

                objects.Add(collision.gameObject);
            }
        }
    }
    protected override IEnumerator EnterCollide()
    {
        isExplosion = true;

        rigid.constraints = RigidbodyConstraints2D.FreezePosition;

        animator.Play(info.hitAnimationName);

        transform.position += new Vector3(0, 1);
        col.offset = new Vector2(0, -0.5f);
        col.size = new Vector2(1.2f, 1.6f);

        yield return null;

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        col.offset = new Vector2(0, 0);
        col.size = new Vector2(0.3f, 0.3f);

        gameObject.SetActive(false);
    }
    protected override IEnumerator Moving()
    {
        yield return new WaitUntil(() => rigid != null);

        rigid.constraints = RigidbodyConstraints2D.None;

        Move();

        yield return null;
    }
}