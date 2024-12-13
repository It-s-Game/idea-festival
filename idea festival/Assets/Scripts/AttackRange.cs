using UnityEngine;
public class AttackRange : MonoBehaviour
{
    private GameObject obj;

    private int damage;

    private void Awake()
    {
        if(!GetComponent<Collider2D>())
        {
            Collider2D col = gameObject.AddComponent<BoxCollider2D>();

            col.isTrigger = true;
        }
    }
    public void Init(GameObject obj)
    {
        this.obj = obj;

        gameObject.SetActive(false);
    }
    private void Start()
    {
    }
    public void Set(int damage)
    {
        this.damage = damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == obj)
        {
            return;
        }
        else if (collision.gameObject.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(damage);
        }
    }
}