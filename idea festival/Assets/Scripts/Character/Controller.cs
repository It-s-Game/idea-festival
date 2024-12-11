using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public abstract class Controller : Character
{
    private const float dash_Duration = 0.4f;

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
    public virtual void LeftStick()
    {
        if(leftStickCoroutine == null)
        {
            if(jumpCount == maxJumpCount)
            {
                animator.Play("run");
            }

            leftStickCoroutine = StartCoroutine(Moving());
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
        if(isAttack)
        {
            return;
        }

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
        if(attackDuration != null || isJump || !enterFloor || inTheDash)
        {
            return;
        }

        isAttack = true;
        attackDuration = StartCoroutine(AttackDuration());
    }
    public virtual void LeftBumper(InputValue value)
    {
        if(dash != null || isAttack || inTheDash)
        {
            return;
        }

        dash = StartCoroutine(Dash());
    }
    protected IEnumerator Moving()
    {
        while (true)
        {
            if(!inTheDash || !isAttack)
            {
                CharacterMove();
            }

            yield return null;
        }
    }
    protected IEnumerator AttackDuration()
    {
        animator.Play("player_attack");

        yield return new WaitForSeconds(so.default_Attack.delay);

        Attack(direction);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        isAttack = false;

        if(enterFloor)
        {
            animator.Play("player_idle");
        }

        yield return new WaitForSeconds(so.default_Attack.coolTime);

        attackDuration = null;
    }
    protected IEnumerator Dash()
    {
        inTheDash = true;

        animator.Play("dash");

        groundDust.SetActive(true);

        Coroutine dash_Moving = StartCoroutine(Dash_Moving());

        yield return new WaitForSeconds(dash_Duration);

        StopCoroutine(dash_Moving);

        if(enterFloor)
        {
            if(leftStickCoroutine == null)
            {
                animator.Play("player_idle");
            }
            else
            {
                animator.Play("run");
            }
        }
        else
        {
            if(!wallSlide.activeSelf)
            {
                animator.Play("double jump");
            }
        }

        inTheDash = false;

        yield return new WaitForSeconds(status.dash_coolTime);

        dash = null;
    }
    protected IEnumerator Dash_Moving()
    {
        moveVec = new Vector3(direction * status.moveSpeed * 1.5f, rigid.velocity.y);

        while (true)
        {
            rigid.velocity = moveVec;

            yield return null;
        }
    }
    public virtual void ButtonB(InputValue value) { }
    public virtual void ButtonY(InputValue value) { }
    public virtual void RightBumper(InputValue value) { }
    public virtual void LeftTrigger(InputValue value) { }
    public virtual void RightTrigger(InputValue value) { }
    public virtual void LeftStickPress(InputValue value) { }
    public virtual void RightStickPress(InputValue value) { }
    protected abstract void Attack(int direction = 0);
}