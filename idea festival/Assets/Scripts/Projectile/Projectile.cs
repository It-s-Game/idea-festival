using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected Projectile_SO so;
    [SerializeField]
    private GameObject initalObject;

    protected List<GameObject> objects = new();

    protected Animator animator;
    protected BoxCollider2D col;
    protected Projectile_Info info;
    protected GameObject obj;

    protected Coroutine move;
    protected Vector3 direction;

    private void Awake()
    {
        if(TryGetComponent(out Animator animator))
        {
            this.animator = animator;
        }

        if(TryGetComponent(out BoxCollider2D col))
        {
            this.col = col;
        }
        else
        {
            this.col = gameObject.AddComponent<BoxCollider2D>();
        }

        Init();

        gameObject.SetActive(false);
    }
    protected virtual void OnEnable()
    {
        objects = new();
    }
    protected virtual void Init()
    {
        col.isTrigger = true;

        info = so.info;
    }
    protected virtual void Update()
    {
        isInvisible();
    }
    public virtual void Set(int direction, GameObject obj)
    {
        transform.position = initalObject.transform.position;

        this.direction = new Vector3(direction, 0);
        this.obj = obj;

        if (direction == 1)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        move = StartCoroutine(Moving());
    }
    private void isInvisible()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if(!GeometryUtility.TestPlanesAABB(planes, col.bounds))
        {
            gameObject.SetActive(false);
        }
    }
    protected virtual void Move()
    {
        transform.position += direction * info.projectileSpeed * Time.deltaTime;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == obj || collision.gameObject.CompareTag("Untagged"))
        {
            return;
        }
        else if(collision.gameObject.TryGetComponent(out IDamagable damagable))
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

        StartCoroutine(EnterCollide());
    }
    protected virtual IEnumerator EnterCollide()
    {
        if (info.stopMove)
        {
            StopCoroutine(move);
        }

        yield return null;

        if(info.hitAnimationName == "")
        {
            gameObject.SetActive(false);

            yield break;
        }

        animator.Play(info.hitAnimationName);

        yield return null;

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        gameObject.SetActive(false);
    }
    protected virtual IEnumerator Moving()
    {
        while (true)
        {
            Move();

            yield return null;
        }
    }
}