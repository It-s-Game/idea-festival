using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class CoolTime
{
    public bool isInCoolTime = false;
}
public abstract class Controller : Character
{
    private CoolTime defaultAttack = new();

    private Vector3 moveVec = new();

    public void Set(InputAction leftStick, int playerIndex)
    {
        if(controller == null)
        {
            controller = this;
        }

        int spawnPointIndex = Managers.Game.spawnPointIndex[playerIndex];

        this.leftStick = leftStick;

        Spawn(spawnPointIndex);
    }
    public void Spawn(int spawnPointIndex)
    {
        transform.position = Managers.Game.mapInfo.SpawnPoints[spawnPointIndex].transform.position;

        if (transform.position.x >= 0)
        {
            direction = 1;

            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        else
        {
            direction = -1;//right

            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
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
    protected void ActiveProjectile(Projectile projectile)
    {
        if(!projectile.gameObject.activeSelf)
        {
            projectile.gameObject.SetActive(true);

            projectile.Set(direction, gameObject);
        }
    }
    private void CharacterMove()
    {
        if(inTheDash || castingSkill)
        {
            return;
        }

        if(!enterWall)
        {
            wallSlide.SetActive(false);
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

            rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;

            leftStickCoroutine = StartCoroutine(Moving());
        }
        else
        {
            rigid.velocity = new Vector3(0, rigid.velocity.y);

            if (!isJump && !castingSkill)
            {
                animator.Play("idle");
            }

            rigid.constraints |= RigidbodyConstraints2D.FreezePositionX;

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

        rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionY;

        if (jumpCount > 0)
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

            rigid.velocity = new Vector2(rigid.velocity.x, jumpHeight);

            jumpCount--;
            isJump = true;
        }
    }
    public virtual void ButtonX(InputValue value)
    {
        Skill(DefaultAttack, "attack", so.default_Attack, defaultAttack);
    }
    public virtual void LeftBumper(InputValue value)
    {
        if(dash != null || wallSlide.activeSelf || castingSkill || inTheDash)
        {
            return;
        }

        dash = StartCoroutine(Dash("dash", status.dash_coolTime));
    }
    protected void Skill(Action skill, string animationName, Attack so, CoolTime inCoolTime)
    {
        if(castingSkill || inTheDash)
        {
            return;
        }

        if(inCoolTime.isInCoolTime)
        {
            return;
        }

        StartCoroutine(CastingSkill(skill, animationName, so, inCoolTime));
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
    protected IEnumerator CastingSkill(Action skill, string animationName, Attack so, CoolTime inCoolTime)
    {
        inCoolTime.isInCoolTime = true;
        castingSkill = true;

        rigid.velocity = new Vector3(0, rigid.velocity.y);

        animator.Play(animationName);

        yield return new WaitForSeconds(so.delay);

        skill.Invoke();

        if(so.isCancelable)
        {
            castingSkill = false;
        }

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        if(!so.isCancelable)
        {
            castingSkill = false;
        }

        if(actionable)
        {
            if(leftStickCoroutine != null)
            {
                animator.Play("run");
            }
            else
            {
                animator.Play("idle");
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

        yield return new WaitForSeconds(so.coolTime);

        inCoolTime.isInCoolTime = false;
    }
    protected IEnumerator Dash(string animationName, float seconds, float magnification = 1.35f, GameObject range = null, CoolTime coolTime = null)
    {
        if(coolTime != null)
        {
            if(coolTime.isInCoolTime)
            {
                yield break;
            }
        }    

        inTheDash = true;

        if(range != null)
        {
            range.SetActive(true);
        }

        animator.Play(animationName);

        groundDust.SetActive(true);

        Coroutine dash_Moving = StartCoroutine(Dash_Moving(magnification));

        rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;

        yield return new WaitForSeconds(0.35f);

        StopCoroutine(dash_Moving);

        rigid.velocity = new Vector3(moveVec.x / magnification, rigid.velocity.y);

        yield return null;

        if (range != null)
        {
            range.SetActive(false);
        }

        if (actionable)
        {
            if(leftStickCoroutine == null)
            {
                animator.Play("idle");
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
                if(jumpCount == 1)
                {
                    animator.Play("jump");
                }
                else
                {
                    animator.Play("double jump");
                }
            }
        }

        if(leftStickCoroutine == null)
        {
            rigid.constraints |= RigidbodyConstraints2D.FreezePositionX;
        }

        inTheDash = false;

        if(coolTime != null)
        {
            coolTime.isInCoolTime = true;
        }

        yield return new WaitForSeconds(seconds);

        if (coolTime != null)
        {
            coolTime.isInCoolTime = false;
        }

        dash = null;
    }
    protected IEnumerator Dash_Moving(float magnification)
    {
        moveVec = new Vector3(rigid.velocity.x + direction * status.moveSpeed * magnification, 0);

        while (true)
        {
            if(rigid.velocity.y > 0)
            {
                moveVec.y = rigid.velocity.y;
            }
            else
            {
                moveVec.y = 0;
            }

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