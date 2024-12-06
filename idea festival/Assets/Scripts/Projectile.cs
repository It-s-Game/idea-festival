using System.Collections;
using UnityEngine;
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Projectile_SO so;

    private Animator animator;
    private GameObject obj;
    private Projectile_Info info;

    private Coroutine move;
    private Vector3 direction;
    private Vector2 position;
    private string animationName;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        Init();

        gameObject.SetActive(false);
    }
    private void Init()
    {
        info = so.info;

        position = transform.position;

        animator.enabled = false;
    }
    public void Set(int direction, GameObject obj)
    {
        this.direction = new Vector3(direction, 0) * info.projectileSpeed * Time.deltaTime;
        this.obj = obj;

        move = StartCoroutine(Move());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == obj)
        {
            return;
        }

        StartCoroutine(AnimationPlaying());
    }
    private IEnumerator Move()
    {
        while(true)
        {
            transform.position += direction;

            yield return null;
        }
    }
    private IEnumerator AnimationPlaying()
    {
        StopCoroutine(move);

        animator.enabled = true;

        yield return null;

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        transform.position = position;

        animator.enabled = false;

        gameObject.SetActive(false);
    }
}