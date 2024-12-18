using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class AttackRange : MonoBehaviour
{
    protected List<GameObject> objects;

    private GameObject obj;

    private int damage = 0;

    private void OnEnable()
    {
        transform.position += new Vector3(0.00001f, 0);
    }
    private void OnDisable()
    {
        objects = new();
    }
    public void Init(GameObject obj, int damage)
    {
        this.obj = obj;
        this.damage = damage;

        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == obj)
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

            damagable.TakeDamage(damage);

            objects.Add(collision.gameObject);
        }
    }
}