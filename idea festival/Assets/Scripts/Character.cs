using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Character : MonoBehaviour, IDamagable
{
    [SerializeField]
    protected CharacterInformation_SO characterInfo;
    
    protected Stat stat;

    protected Rigidbody2D rigid;
    protected Animator animator;
    protected Collider2D col;

    protected int jumpCount = maxJumpCount;
    protected int playerIndex;

    private const int maxJumpCount = 2;

    private int health;

    public int PlayerIndex { get { return playerIndex; } }
    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if(TryGetComponent(out Collider2D col))
        {
            this.col = col;
        }
        else
        {
            this.col = transform.AddComponent<CapsuleCollider2D>();
        }
    }
    protected virtual void Start()
    {
        //Init();
    }
    private void Init()
    {
        stat = characterInfo.stat;
        health = stat.maxHealth;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            //die
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("wall"))
        {
            animator.Play("wallslide");
        }
    }
}