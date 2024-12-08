using System.Collections;
using UnityEngine;
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Projectile_SO so;
    [SerializeField]
    private GameObject initalObject;
    [SerializeField]
    private string hitAnimationName = "";

    private Animator animator;
    private GameObject obj;
    private Projectile_Info info;

    private Coroutine move;
    private Vector3 direction;

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
    public void Set(int direction, GameObject obj)
    {
        this.direction = new Vector3(direction, 0);
        this.obj = obj;

        move = StartCoroutine(Moving());
    }
    protected virtual void Move()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == obj)
        {
            return;
        }

        StartCoroutine(Collide());
    }
    protected virtual IEnumerator Collide()
    {
        if(hitAnimationName == "")
        {
            yield break;
        }

        animator.Play(hitAnimationName);

        yield return null;

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        transform.position = initalObject.transform.position;

        animator.enabled = false;

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
}