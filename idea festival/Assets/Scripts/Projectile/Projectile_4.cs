using UnityEngine;
public class Projectile_4 : Projectile
{
    private float yPos;
    private bool isInvisible = false;

    protected override void OnEnable()
    {
        base.OnEnable();

        yPos = obj.transform.position.y;
    }
    protected override void Move()
    {
        transform.position += new Vector3(direction.x, -4);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == obj || collision.gameObject.CompareTag("Untagged"))
        {
            return;
        }
        else if (collision.gameObject.TryGetComponent(out IDamagable damagable))
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

            StartCoroutine(EnterCollide());
        }
    }
}