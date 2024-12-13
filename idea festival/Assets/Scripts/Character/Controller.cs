using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public abstract class Controller : Character
{
    private Vector3 moveVec = new();
    private bool defaultAttack = false;

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
    protected void ActiveProjectile(Projectile[] projectiles)
    {
        foreach(Projectile projectile in projectiles)
        {
            if(!projectile.gameObject.activeSelf)
            {
                projectile.gameObject.SetActive(true);

                projectile.Set(direction, gameObject);

                break;
            }
        }
    }
    private void CharacterMove()
    {
        if(inTheDash || castingSkill)

        {
            return;
        }

        if ((Mathf.Sign(leftStick.ReadValue<Vector2>().x)) != direction)
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

            if (enterWall)
            {
                return;
            }

            if (jumpCount == maxJumpCount)
            {
                animator.Play("run");
            }
        }

        if(wallSlide.activeSelf)
        {
            moveVec = new Vector3(direction * status.moveSpeed, rigid.velocity.y * 0.9f);
        }
        else
        {
            moveVec = new Vector3(direction * status.moveSpeed, rigid.velocity.y);
        }

        rigid.velocity = moveVec;
    }
    public virtual void LeftStick()
    {
        if(leftStickCoroutine == null)
        {
            if(jumpCount == maxJumpCount)
            {
                if(!castingSkill)
                {
                    animator.Play("run");
                }
            }

            leftStickCoroutine = StartCoroutine(Moving());
        }
        else
        {
            rigid.velocity = new Vector3(0, rigid.velocity.y);

            if (!isJump)
            {
                animator.Play("player_idle");
            }

            StopCoroutine(leftStickCoroutine);

            leftStickCoroutine = null;
        }
    }
    public virtual void ButtonA(InputValue value)
    {
        if(castingSkill)
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
        if(castingSkill || isJump || !enterFloor || inTheDash)
        {
            return;
        }

        if(!defaultAttack)
        {
            defaultAttack = true;
        }

        StartCoroutine(CastingSkill(DefaultAttack, "player_attack", so.default_Attack.delay, defaultAttack));
    }
    public virtual void LeftBumper(InputValue value)
    {
        if(dash != null || wallSlide.activeSelf || castingSkill || inTheDash)
        {
            return;
        }

        dash = StartCoroutine(Dash());
    }
    protected void Skill(Action skill, string animationName, float delay, ref bool inCoolTime)
    {
        if (castingSkill || !enterFloor || inTheDash)
        {
            return;
        }

        if (!inCoolTime)
        {
            inCoolTime = true;
        }

        StartCoroutine(CastingSkill(skill, animationName, delay, inCoolTime));
    }
    protected IEnumerator Moving()
    {
        while (true)
        {
            if(!inTheDash || !castingSkill)
            {
                CharacterMove();
            }

            yield return null;
        }
    }
    protected IEnumerator CastingSkill(Action skill, string animationName, float delay, bool inCoolTime)
    {
        castingSkill = true;

        rigid.velocity = new Vector3(0, rigid.velocity.y);

        animator.Play(animationName);

        yield return new WaitForSeconds(delay);

        skill.Invoke();

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        castingSkill = false;

        if(enterFloor)
        {
            if(leftStickCoroutine != null)
            {
                animator.Play("run");
            }
            else
            {
                animator.Play("player_idle");
            }
        }
        else
        {
            if(enterWall)
            {
                animator.Play("wallslide");
            }
            else
            {
                animator.Play("jump");
            }
        }

        yield return new WaitForSeconds(so.default_Attack.coolTime);

        inCoolTime = false;
    }
    protected IEnumerator Dash()
    {
        inTheDash = true;

        animator.Play("dash");

        groundDust.SetActive(true);

        Coroutine dash_Moving = StartCoroutine(Dash_Moving());

        yield return new WaitForSeconds(0.35f);

        StopCoroutine(dash_Moving);

        while (Mathf.Abs(rigid.velocity.x) >= status.moveSpeed)
        {
            yield return null;
        }

        if (enterFloor)
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
        moveVec = new Vector3(direction * status.moveSpeed * 1.75f, 0);

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
    protected abstract void DefaultAttack();
}