using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class CharacterController : Character
{
    protected Character character;

    protected InputAction leftStick = null;
    protected InputAction rightStick = null;

    private PlayerInput playerInput;

    protected override void Awake()
    {
        base.Awake();

        playerInput = GetComponent<PlayerInput>();

        if (playerInput.actions == null)
        {
            playerInput.actions = Resources.Load<InputActionAsset>("CharacterControll");
        }

        playerInput.actions.Enable();

        Set();
    }
    private void Set()
    {
        leftStick = playerInput.actions["LeftStick"];

        leftStick.started += (ctx =>
        {
            LeftStick(ctx);
        });
        leftStick.canceled += (ctx =>
        {
            LeftStick(ctx);
        });

        rightStick = playerInput.actions["RightStick"];

        rightStick.started += (ctx =>
        {
            RightStick(ctx);
        });
        rightStick.canceled += (ctx =>
        {
            RightStick(ctx);
        });

        /*
         
        direction = vec.x > 0 ? 1 : -1;

         */
    }
    protected virtual void LeftStick(InputAction.CallbackContext value) { LeftStick(value); }
    protected virtual void RightStick(InputAction.CallbackContext value) { }
    protected virtual void OnLeftStick(InputValue value) { }
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