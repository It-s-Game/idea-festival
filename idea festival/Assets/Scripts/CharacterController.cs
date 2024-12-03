using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class CharacterController : Character
{
    private Coroutine leftStickCoroutine = null;
    private InputAction leftStick = null;
    private InputAction rightStick = null;

    private PlayerInput playerInput;
    private Coroutine attackDuration;
    private Vector3 directionVec = new();
    private int direction;

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
        leftStick.performed += (ctx =>
        {
            if(jumpCount == maxJumpCount)
            {
                animator.Play("run");
            }
;        });
        leftStick.canceled += (ctx =>
        {
            LeftStick(ctx);
        });
    }
    private void CharacterMove()
    {
        direction = leftStick.ReadValue<Vector2>().x > 0 ? 1 : -1;
        directionVec = new Vector3(direction * status.moveSpeed, rigid.velocity.y);

        rigid.velocity = directionVec;

        if (direction == 1)
        {
            directionVec = new Vector3(0, 0);
        }
        else
        {
            directionVec = new Vector3(0, 180);
        }

        transform.rotation = Quaternion.Euler(directionVec);
    }
    protected virtual void LeftStick(InputAction.CallbackContext value)
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
    protected virtual void OnButtonA(InputValue value)
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
    protected virtual void OnButtonX(InputValue value)
    {
        if(attackDuration == null)
        {
            attackDuration = StartCoroutine(AttackDuration());
        }
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
        if(isJump)
        {
            yield break;
        }

        if(status.jumpAttack)
        {
            if (isJump == true)
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
    protected virtual void OnButtonB(InputValue value) { }
    protected virtual void OnButtonY(InputValue value) { }
    protected virtual void OnLeftBumper(InputValue value) { }
    protected virtual void OnRightBumper(InputValue value) { }
    protected virtual void OnLeftTrigger(InputValue value) { }
    protected virtual void OnRightTrigger(InputValue value) { }
    protected virtual void OnLeftStickPress(InputValue value) { }
    protected virtual void OnRightStickPress(InputValue value) { }
}