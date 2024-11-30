using UnityEngine.InputSystem;
public class Archer_Hero : CharacterController
{
    protected override void Start()
    {
        base.Start();

        Set();
    }
    protected override void OnButtonA(InputValue value)
    {
        animator.Play("jump");
    }
}