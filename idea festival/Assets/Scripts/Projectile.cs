using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public abstract class Projectile : MonoBehaviour
{
    [SerializeField]
    protected Projectile_SO so;
    [SerializeField]
    private GameObject initalObject;

    protected List<GameObject> objects = new();

    protected Animator animator;
    protected Collider2D col;
    protected Projectile_Info info;

    protected Coroutine move;
    protected Vector3 direction;

    private GameObject obj;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if(TryGetComponent(out Collider2D col))
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
    protected void OnEnable()
    {
        objects = new();
    }
    private void Init()
    {
        info = so.info;
    }
    public virtual void Set(int direction, GameObject obj)
    {
        transform.position = initalObject.transform.position;

        this.direction = new Vector3(direction, 0);
        this.obj = obj;

        move = StartCoroutine(Moving());
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == obj)
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

        StartCoroutine(Collide());
    }
    protected virtual IEnumerator Collide()
    {
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
    protected abstract void Move();
}