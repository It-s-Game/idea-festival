using System.Collections;
using UnityEngine;
public class Projectile_3 : Projectile
{
    [SerializeField]
    private Vector2 offset1;
    [SerializeField]
    private Vector2 offset2;
    [SerializeField]
    private Vector2 size1;
    [SerializeField]
    private Vector2 size2;
    [SerializeField]
    private Vector3 increasePosition;

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
        if (collision.gameObject == obj || collision.gameObject.CompareTag("Untagged"))
        {
            return;
        }

        if(!isExplosion)
        {
            if (collision.gameObject.CompareTag("floor"))
            {
                if(collide == null)
                {
                    collide = StartCoroutine(EnterCollide());
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == obj || collision.gameObject.CompareTag("Untagged"))
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

        transform.position += increasePosition;
        col.offset = offset2;
        col.size = size2;

        yield return null;

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        col.offset = offset1;
        col.size = size1;

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