using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : PlayerController
{
    PlayerInformation_SO playerInfo;

    protected Rigidbody2D rigid;
    protected Animator animator;
    protected Collider2D col;

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

        Init();
    }
    private void Init()
    {
        //stat
    }
    protected void PlayerMove(Vector3 direction)
    {
        //need stat.moveSpeed
    }
}