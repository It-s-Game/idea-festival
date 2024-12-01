using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class CharacterController : Character
{
    protected virtual void OnButtonB(InputValue value) { }
    protected virtual void OnButtonY(InputValue value) { }
    protected virtual void OnButtonA(InputValue value) { }
    protected virtual void OnButtonX(InputValue value) { }
    protected virtual void OnLeftBumper(InputValue value) { }
    protected virtual void OnRightBumper(InputValue value) { }
    protected virtual void OnLeftTrigger(InputValue value) { }
    protected virtual void OnRightTrigger(InputValue value) { }
    protected virtual void OnLeftStickPress(InputValue value) { }
    protected virtual void OnRightStickPress(InputValue value) { }
}