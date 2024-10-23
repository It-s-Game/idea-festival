using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Character : CharacterController/*, IDamagable, IDamage*/
{
    protected CharacterInformation_SO characterInfo;
    protected Stat stat;

    protected Rigidbody2D rigid;
    protected Animator animator;
    protected Collider2D col;

    private Coroutine coroutine = null;

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
    private void CharacterMove(Vector3 vec)
    {
        int direction = vec.x > 0 ? 1 : -1;

        transform.position += new Vector3(direction, 0) * 4 * Time.deltaTime;
    }
    protected override void LeftStick(InputAction.CallbackContext value)
    {
        if(coroutine == null)
        {
            coroutine = StartCoroutine(LeftStick());
        }
        else
        {
            StopCoroutine(coroutine);

            coroutine = null;
        }
    }
    protected override void OnButtonA(InputValue value)
    {
        //Debug.Log("a");
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