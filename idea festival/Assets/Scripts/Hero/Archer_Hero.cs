using UnityEngine.InputSystem;
public class Archer_Hero : CharacterController
{
    public override void OnButtonA(InputValue value)
    {
        animator.Play("jump");
    }
}