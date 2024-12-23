using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Character : MonoBehaviour, IDamagable
{
    [SerializeField]
    protected Character_SO so;
    [SerializeField]
    protected GameObject deathSmoke;
    [SerializeField]
    protected GameObject groundDust;
    [SerializeField]
    protected GameObject wallSlide;
    
    protected Status status;

    protected Rigidbody2D rigid;
    protected Animator animator;
    protected Collider2D col;
    protected InputAction leftStick = null;

    protected const int maxJumpCount = 2;

    protected Coroutine leftStickCoroutine = null;
    protected Coroutine dash = null;
    protected Vector3 jumpHeight;//
    protected int jumpCount = maxJumpCount;
    protected int direction = 0;
    protected bool isJump = false;
    protected bool inTheDash = false;
    protected bool isAttack = false;
    protected bool enterWall = false;
    protected bool enterFloor = true;

    private int health;
    
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
            this.col = transform.AddComponent<BoxCollider2D>();
        }
    }
    protected virtual void Start()
    {
        Init();
    }
    private void Init()
    {
        status = so.status;
        health = status.maxHealth;

        jumpHeight = new Vector3(0, status.jumpHeight);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            animator.Play("die");

            deathSmoke.SetActive(true);
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("floor"))
        {
            jumpCount = maxJumpCount;
            isJump = false;

            if(leftStickCoroutine == null)
            {
                animator.Play("player_idle");
            }
            else
            {
                animator.Play("run");
            }

            groundDust.gameObject.SetActive(true);

            if(wallSlide.activeSelf)
            {
                if(direction == -1)
                {
                    direction = 1;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                else
                {
                    direction = -1;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }

                animator.Play("player_idle");

                wallSlide.SetActive(false);
            }

            enterFloor = true;
        }
        else if (collision.gameObject.CompareTag("wall"))
        {
            enterWall = true;

            if(enterFloor)
            {
                return;
            }

            animator.Play("wallslide");

            wallSlide.SetActive(true);
        }
    }
    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("floor"))
        {
            enterFloor = false;
        }
        else if(collision.gameObject.CompareTag("wall"))
        {
            enterWall = false;

            if(isJump)
            {
                animator.Play("jump");

                wallSlide.SetActive(false);
            }
        }
    }
}