using System.Collections;
using UnityEngine;
public abstract class Projectile : MonoBehaviour
{
    [SerializeField]
    protected Projectile_SO so;
    [SerializeField]
    private GameObject initalObject;

    protected Animator animator;
    protected Projectile_Info info;

    protected Coroutine move;
    protected Vector3 direction;

    private GameObject obj;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        Init();

        gameObject.SetActive(false);
    }
    private void Init()
    {
        info = so.info;
    }
    public virtual void Set(int direction, GameObject obj)
    {
        this.direction = new Vector3(direction, 0);
        this.obj = obj;

        move = StartCoroutine(Moving());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == obj)
        {
            return;
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(info.damage);
            }
        }

        StartCoroutine(Collide());
    }
    protected virtual IEnumerator Collide()
    {
        if(info.hitAnimationName == "")
        {
            yield break;
        }

        animator.Play(info.hitAnimationName);

        yield return null;

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        transform.position = initalObject.transform.position;

        gameObject.SetActive(false);
    }
    private IEnumerator Moving()
    {
        while (true)
        {
            Move();

            yield return null;
        }
    }
    protected abstract void Move();
}