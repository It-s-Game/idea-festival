using UnityEngine;
public class Projectile_4 : Projectile
{
    private GameObject targetObject = null;

    private Vector2 overlapSize;
    private float height;
    private float width;
    private float yPos;
    private bool isInvisible = false;

    protected override void OnEnable()
    {
        base.OnEnable();

        targetObject = null;
    }
    private void Start()
    {
        height = Camera.main.orthographicSize * 2;
        width = height * Camera.main.aspect;

        overlapSize = new Vector2(width, height);
    }
    public override void Set(int direction, GameObject obj)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(obj.transform.position, overlapSize, LayerMask.GetMask("Player"));

        float minDistance = 0;

        foreach(Collider2D col in colliders)
        {
            if(targetObject == null)
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

        transform.position = new Vector2(targetObject.transform.position.x, height + 2);
    }
    private float GetDistance(GameObject go, out float result)
    {
        float distance = (go.transform.position - obj.transform.position).magnitude;

        return result = distance;
    }
    protected override void Move()
    {
        transform.position += new Vector3(0, -4) * info.projectileSpeed * Time.deltaTime;
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

        StartCoroutine(EnterCollide());
    }
}