using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Character : CharacterController
{
    protected CharacterInformation_SO characterInfo;
    protected Stat stat;

    protected Rigidbody2D rigid;
    protected Animator animator;
    protected Collider2D col;

    private int health;

    protected override void Awake()
    {
        base.Awake();

        character = this;

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
    protected void CharacterMove(Vector3 direction)
    {
        transform.position += direction * 1 * Time.deltaTime;
    }
    protected override void OnLeftStick(InputValue context)
    {
        CharacterMove(context.Get<Vector2>());
    }
}