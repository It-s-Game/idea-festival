using System.Collections;
using UnityEngine;
public class Projectile_5 : Projectile
{
    private Rigidbody2D rigid = null;

    protected override void OnEnable()
    {
        base.OnEnable();
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
        if (collision.gameObject == obj || collision.gameObject.CompareTag("Untagged"))
        {
            return;
        }

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

            gameObject.SetActive(false);
        }
    }
    protected override IEnumerator Moving()
    {
        yield return new WaitUntil(() => rigid != null);

        rigid.constraints = RigidbodyConstraints2D.None;

        Move();

        yield return null;
    }
}