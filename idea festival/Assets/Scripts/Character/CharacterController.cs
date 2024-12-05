using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class CharacterController : Character
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
        if (jumpCount == maxJumpCount)
        {
            animator.Play("run");
        }

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
    public virtual void ButtonA(InputValue value)
    {
        if(jumpCount > 0)
        {
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
    public virtual void ButtonX(InputValue value)
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
            }
            else
            {
                animator.Play("player_attack");
            }
        }
        else
        {
            animator.Play("player_attack");
        }

        yield return new WaitForSeconds(status.attackDelay);

        attackDuration = null;
    }
    public virtual void ButtonB(InputValue value) { }
    public virtual void ButtonY(InputValue value) { }
    public virtual void LeftBumper(InputValue value) { }
    public virtual void RightBumper(InputValue value) { }
    public virtual void LeftTrigger(InputValue value) { }
    public virtual void RightTrigger(InputValue value) { }
    public virtual void LeftStickPress(InputValue value) { }
    public virtual void RightStickPress(InputValue value) { }
}