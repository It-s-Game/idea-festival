using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(BoxCollider2D))]
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
    protected Controller controller = null;
    protected InputAction leftStick = null;

    protected const int maxJumpCount = 2;

    protected Coroutine leftStickCoroutine = null;
    protected Coroutine dash = null;
    protected float stamina = 1;
    protected float jumpHeight;
    protected int jumpCount = maxJumpCount;
    protected int direction = 0;
    protected int life = 3;
    protected bool isJump = false;
    protected bool inTheDash = false;
    protected bool castingSkill = false;
    protected bool enterWall = false;
    protected bool enterFloor = false;
    protected bool actionable = true;

    private Coroutine dieing = null;
    private float health;

    public float Health
    {
        get => health;
    }
    public float MaxHealth
    {
        get => status.maxHealth;
    }

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }
    protected virtual void Start()
    {
        status = so.status;

        Init();
    }
    private void Init()
    {
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;

        health = status.maxHealth;
        jumpHeight = status.jumpHeight;
    }
    protected void Update()
    {
        if(dieing == null)
        {
            isInvisible();
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage * stamina;

        if (health <= 0)
        {
            StopAllCoroutines();

            rigid.gravityScale = 1.5f;

            animator.Play("die");

            dieing = StartCoroutine(Dieing());

            deathSmoke.SetActive(true);

            rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        }
    }
    private void isInvisible()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if (!GeometryUtility.TestPlanesAABB(planes, col.bounds))
        {
            TakeDamage(MaxHealth);
        }
    }
    private IEnumerator CollisionEnter()
    {
        yield return new WaitUntil(() => castingSkill == false);

        if (leftStickCoroutine == null)
        {
            animator.Play("idle");
        }
        else
        {
            animator.Play("run");
        }

        if (wallSlide.activeSelf)
        {
            if (direction == -1)
            {
                direction = 1;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                direction = -1;
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }

            animator.Play("idle");

            wallSlide.SetActive(false);
        }

        if(!isJump)
        {
            rigid.constraints |= RigidbodyConstraints2D.FreezePositionY;
        }

        rigid.velocity = new Vector3(rigid.velocity.x, 0);

        jumpCount = maxJumpCount;
        isJump = false;
        actionable = true;
    }
    private IEnumerator CollisionEnterWall()
    {
        yield return new WaitUntil(() => castingSkill == false);

        jumpCount = maxJumpCount;
        enterWall = true;

        if (actionable)
        {
            yield break;
        }

        animator.Play("wallslide");

        wallSlide.SetActive(true);
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("floor"))
        {
            StartCoroutine(CollisionEnter());

            enterFloor = true;
            groundDust.gameObject.SetActive(true);
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject != gameObject && !actionable)
            {
                StartCoroutine(CollisionEnter());
            }
            else if (collision.gameObject.CompareTag("wall"))
            {
                StartCoroutine(CollisionEnterWall());
            }
        }
        else if (collision.gameObject.CompareTag("wall"))
        {
            StartCoroutine(CollisionEnterWall());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(health > 0)
        {
            return;
        }

        if(collision.gameObject.CompareTag("floor"))
        {
            rigid.constraints |= RigidbodyConstraints2D.FreezePositionY;
        }

        if(dieing == null)
        {
            rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        }
    }
    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("floor"))
        {
            rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionY;

            enterFloor = false;
            actionable = false;
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject != gameObject && !enterFloor)
            {
                rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionY;

                actionable = false;
            }
        }

        if(collision.gameObject.CompareTag("wall"))
        {
            wallSlide.SetActive(false);

            enterWall = false;

            if(isJump)
            {
                animator.Play("jump");

                wallSlide.SetActive(false);
            }
        }
    }
    private IEnumerator Dieing()
    {
        yield return null;

        col.isTrigger = true;

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionY;

        if(life > 0)
        {
            life--;

            col.isTrigger = false;

            rigid.gravityScale = 1;
            rigid.velocity = Vector2.zero;

            Init();
            controller.Spawn(Random.Range(0, 4));

            yield break;
        }

        dieing = null;

        while (true)
        {
            if(transform.position.y < -100)
            {
                gameObject.SetActive(false);
            }

            rigid.velocity = new(0, -1);

            yield return null;
        }
    }
}