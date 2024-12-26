using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Shield : MonoBehaviour, IDamagable
{
    [SerializeField]
    private GameObject initalObject;

    private Rigidbody2D rigid;
    private Animator animator;
    private Collider2D col;

    private const int maxHealth = 2;

    private int health = maxHealth;
    private int direction;

    public void TakeDamage(int damage)
    {
        health--;

        if(health == 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if(TryGetComponent(out Collider2D col))
        {
            this.col = col;
        }
        else
        {
            col = gameObject.AddComponent<BoxCollider2D>();

            col.isTrigger = true;
        }
    }
    private void OnEnable()
    {
        transform.position = initalObject.transform.position;
        health = maxHealth;
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void Set(int direction)
    {
        this.direction = direction;

        if (direction == 1)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }
    private void Update()
    {
        rigid.velocity = new Vector3(direction, 0);

        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            gameObject.SetActive(false);
        }
    }
}