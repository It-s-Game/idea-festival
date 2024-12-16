using System.Collections.Generic;
using UnityEngine;
public class AttackRange : MonoBehaviour
{
    protected List<GameObject> objects;

    private GameObject obj;

    private int damage;

    protected void Awake()
    {
        if(!GetComponent<Collider2D>())
        {
            Collider2D col = gameObject.AddComponent<BoxCollider2D>();

            col.isTrigger = true;
        }

        gameObject.SetActive(false);
    }
    protected void OnEnable()
    {
        objects = new();
    }
    public void Init(GameObject obj)
    {
        this.obj = obj;

        gameObject.SetActive(false);
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
            foreach(GameObject go in objects)
            {
                if(collision.gameObject == go)
                {
                    return;
                }
            }

            damagable.TakeDamage(damage);

            objects.Add(collision.gameObject);
        }
    }
}