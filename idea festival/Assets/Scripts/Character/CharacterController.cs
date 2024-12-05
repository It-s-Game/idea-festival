using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public abstract class CharacterController : Character
{
    private Coroutine leftStickCoroutine = null;
    private InputAction leftStick = null;

    private PlayerInput playerInput;
    private Coroutine attackDuration;
    private Vector3 moveVec = new();
    private int direction = 0;
    
    protected override void Awake()
    {
        base.Awake();

        playerInput = GetComponent<PlayerInput>();

        playerInput.actions.Enable();
    }
    protected override void Start()
    {
        base.Start();

        leftStick = playerInput.actions["LeftStick"];

        leftStick.started += (ctx =>
        {
            LeftStick(ctx);
        });
        leftStick.canceled += (ctx =>
        {
            LeftStick(ctx);
        });
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(status.maxHealth);
        }
    }
    private void CharacterMove()
    {
        if ((Mathf.Sign(leftStick.ReadValue<Vector2>().x)) != direction )
        {
            direction = (int)Mathf.Sign(leftStick.ReadValue<Vector2>().x);

            if (direction == 1)
            {
                moveVec = new Vector3(0, 0);
            }
            else
            {
                moveVec = new Vector3(0, 180);
            }

            transform.rotation = Quaternion.Euler(moveVec);

            if (jumpCount == maxJumpCount)
            {
                animator.Play("run");
            }
        }

        moveVec = new Vector3(direction * status.moveSpeed, rigid.velocity.y);

        rigid.velocity = moveVec;
    }
    public virtual void LeftStick(InputAction.CallbackContext value)
    {
        if (leftStickCoroutine == null)
        {
            if(jumpCount == maxJumpCount)
            {
                animator.Play("run");
            }

            leftStickCoroutine = StartCoroutine(LeftStick());
        }
        else
        {
            rigid.velocity = Vector2.zero;

            if(!isJump)
            {
                animator.Play("player_idle");
            }

            StopCoroutine(leftStickCoroutine);

            leftStickCoroutine = null;
        }
    }
    public virtual void OnButtonA(InputValue value)
    {
        if(jumpCount > 0)
        {
            if(wallSlide.activeSelf)
            {
                return;
            }

            if(jumpCount == 2)
            {
                animator.Play("jump");
            }
            else if(jumpCount == 1)
            {
                animator.Play("double jump");
            }

            rigid.AddForce(jumpHeight, ForceMode2D.Impulse);

            jumpCount--;
            isJump = true;
        }
    }
    public virtual void OnButtonX(InputValue value)
    {
        if(attackDuration != null || isJump)
        {
            return;
        }

        attackDuration = StartCoroutine(AttackDuration());
    }
    protected IEnumerator LeftStick()
    {
        while (true)
        {
            CharacterMove();

            yield return null;
        }
    }
    protected IEnumerator AttackDuration()
    {
        if(status.jumpAttack)
        {
            if(isJump == true)
            {
                animator.Play("jump attack");

                attackDuration = null;

                yield break;
            }
        }

        animator.Play("player_attack");

        Attack(direction);

        yield return new WaitForSeconds(status.attackDelay);

        attackDuration = null;
    }
    protected abstract void Attack(int direction = 0);
    public virtual void OnButtonB(InputValue value) { }
    public virtual void OnButtonY(InputValue value) { }
    public virtual void OnLeftBumper(InputValue value) { }
    public virtual void OnRightBumper(InputValue value) { }
    public virtual void OnLeftTrigger(InputValue value) { }
    public virtual void OnRightTrigger(InputValue value) { }
    public virtual void OnLeftStickPress(InputValue value) { }
    public virtual void RightStickPress(InputValue value) { }
}