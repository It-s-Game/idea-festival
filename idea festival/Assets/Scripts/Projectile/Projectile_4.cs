using System.Collections;
using UnityEngine;
public class Projectile_4 : Projectile
{
    private GameObject targetObject = null;

    private Vector2 overlapSize;
    private float height;
    private float width;

    protected override void OnEnable()
    {
        base.OnEnable();

        targetObject = null;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));

        col.offset = new Vector2(0.025f, -0.0175f);
        col.size = new Vector2(1.25f, 0.4f);
    }
    protected override void Init()
    {
        height = Camera.main.orthographicSize * 2;
        width = height * Camera.main.aspect;

        overlapSize = new Vector2(width, height);

        base.Init();
    }
    public override void Set(int direction, GameObject obj)
    {
        this.obj = obj;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(Vector2.zero, overlapSize, 0, LayerMask.GetMask("Character"));

        float minDistance = 0;

        foreach(Collider2D col in colliders)
        {
            if(col.gameObject == obj || col.gameObject == gameObject)
            {
                continue;
            }
            else if(targetObject == null)
            {
                GetDistance(col.gameObject, out minDistance);

                targetObject = col.gameObject;
            }
            else if(minDistance > GetDistance(col.gameObject, out float distance))
            {
                minDistance = distance;
                targetObject = col.gameObject;
            }
        }

        if(targetObject == null)
        {
            return;
        }

        transform.position = new Vector2(targetObject.transform.position.x, height);

        move = StartCoroutine(Moving());
    }
    protected override void Update()
    {
        
    }
    private float GetDistance(GameObject go, out float result)
    {
        float distance = (go.transform.position - obj.transform.position).magnitude;

        return result = distance;
    }
    protected override void Move()
    {
        transform.position += new Vector3(0, -1) * info.projectileSpeed * Time.deltaTime;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == obj || collision.gameObject.CompareTag("Untagged") || collision.gameObject.CompareTag("wall"))
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

        if(collide == null)
        {
            collide = StartCoroutine(EnterCollide());
        }
    }
    protected override IEnumerator EnterCollide()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        transform.position += new Vector3(0, 0.5f);
        col.offset = new Vector2(-0.15f, -1);
        col.size = new Vector2(1.8f, 2.3f);

        return base.EnterCollide();
    }
}