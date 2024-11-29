using UnityEngine.InputSystem;
public class ArcherHero : Character
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