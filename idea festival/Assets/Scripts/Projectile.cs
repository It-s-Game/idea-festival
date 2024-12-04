using System.Collections;
using UnityEngine;
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Projectile_SO so;

    private Animator animator;
    private GameObject obj;
    private Projectile_Info info;

    private Vector3 direction;
    private string animationName;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Init();
    }
    private void Init()
    {
        info = so.info;
    }
    public void Set(int direction, GameObject obj)
    {
        this.direction = new Vector3(direction, 0) * info.projectileSpeed * Time.deltaTime;
        this.obj = obj;
    }
    private void Update()
    {
        transform.position += direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == obj)
        {
            return;
        }

        StartCoroutine(AnimationPlaying());
    }
    private IEnumerator AnimationPlaying()
    {
        animator.Play(animationName);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        gameObject.SetActive(false);
    }
}