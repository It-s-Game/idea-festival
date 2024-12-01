using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Character : MonoBehaviour, IDamagable
{
    [SerializeField]
    protected CharacterInformation_SO characterInfo;
    
    protected Stat stat;

    protected Rigidbody2D rigid;
    protected Animator animator;
    protected Collider2D col;

    protected int playerIndex;

    private Coroutine leftStickCoroutine = null;
    private InputAction leftStick = null;
    private InputAction rightStick = null;
    private PlayerInput playerInput;

    private int health;
    private int direction;

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

        playerInput.actions.Enable();
    }
    protected virtual void Start()
    {
        //Init();

        leftStick = playerInput.actions["LeftStick"];

        leftStick.started += (ctx =>
        {
            LeftStick(ctx);
        });
    }
    private void Init()
    {
        stat = characterInfo.stat;
        health = stat.maxHealth;
    }
    private void CharacterMove()
    {
        transform.position += new Vector3(direction, 0) * 4 * Time.deltaTime;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            //die
        }
    }
    protected virtual void LeftStick(InputAction.CallbackContext value)
    {
        if(leftStickCoroutine == null)
        {
            leftStickCoroutine = StartCoroutine(LeftStick());
        }
        else
        {
            StopCoroutine(leftStickCoroutine);

            leftStickCoroutine = null;
        }
    }
    protected IEnumerator LeftStick()
    {
        while(true)
        {
            CharacterMove();

            yield return null;
        }
    }
}