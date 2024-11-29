using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Character : CharacterController, IDamagable
{
    [SerializeField]
    protected CharacterInformation_SO characterInfo;
    
    protected Stat stat;

    protected Rigidbody2D rigid;
    protected Animator animator;
    protected Collider2D col;

    private Coroutine leftStickCoroutine = null;

    //private const float 

    private int health;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            //die
        }
    }
    protected override void Awake()
    {
        base.Awake();

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
    protected override void Start()
    {
        base.Start();

        //Init();
    }
    private void Init()
    {
        stat = characterInfo.stat;
        health = stat.maxHealth;
    }
    private void CharacterMove(Vector3 vec)
    {
        int direction = vec.x > 0 ? 1 : -1;

        transform.position += new Vector3(direction, 0) * 4 * Time.deltaTime;
    }
    protected void Set()
    {
        character = this;
    }
    protected override void LeftStick(InputAction.CallbackContext value)
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
    private IEnumerator LeftStick()
    {
        while(true)
        {
            CharacterMove(leftStick.ReadValue<Vector2>());

            yield return null;
        }
    }
}