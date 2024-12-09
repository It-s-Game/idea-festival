using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public abstract class Controller : Character
{
    private Coroutine leftStickCoroutine = null;

    private Coroutine attackDuration;
    private Vector3 moveVec = new();
    private int direction = 0;

    public void Set(InputAction leftStick, int playerIndex)
    {
        this.leftStick = leftStick;

        if ((playerIndex + 1) % 2 == 0)
        {
            direction = -1;

            //transform.position
        }
        else
        {
            direction = 1;

            //transform.position
        }
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
        if((Mathf.Sign(leftStick.ReadValue<Vector2>().x)) != direction)
        {
            direction = (int)Mathf.Sign(leftStick.ReadValue<Vector2>().x);

            if(direction == 1)
            {
                moveVec = new Vector3(0, 0);
            }
            else
            {
                moveVec = new Vector3(0, 180);
            }

            transform.rotation = Quaternion.Euler(moveVec);

            if(jumpCount == maxJumpCount)
            {
                animator.Play("run");
            }
        }

        moveVec = new Vector3(direction * status.moveSpeed, rigid.velocity.y);

        rigid.velocity = moveVec;
    }
    public virtual void LeftStick(InputAction.CallbackContext value)
    {
        if(leftStickCoroutine == null)
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

                attackDuration = null;

                yield break;
            }
        }

        animator.Play("player_attack");

        yield return new WaitForSeconds(status.defaultAttack);

        Attack(direction);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        animator.Play("player_idle");

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
    protected abstract void Attack(int direction = 0);
}