using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class CharacterController : MonoBehaviour
{
    protected Character character;

    protected InputAction leftStick = null;
    protected InputAction rightStick = null;

    private PlayerInput playerInput;

    private int playerIndex;

    public int PlayerIndex { get { return playerIndex; } }
    protected virtual void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerInput.actions == null)
        {
            playerInput.actions = Resources.Load<InputActionAsset>("CharacterController");
        }

        playerInput.actions.Enable();

        Set();
    }
    private void Set()
    {
        //Left Stick Started and Canceled
        leftStick = playerInput.actions["LeftStick"];

        leftStick.started += (ctx =>
        {
            LeftStick(ctx);
        });
        leftStick.canceled += (ctx =>
        {
            LeftStick(ctx);
        });

        //Right Stick Started and Canceled
        rightStick = playerInput.actions["RightStick"];

        rightStick.started += (ctx =>
        {
            RightStick(ctx);
        });
        rightStick.canceled += (ctx =>
        {
            RightStick(ctx);
        });
    }
    protected virtual void Start()
    {
        playerIndex = playerInput.playerIndex;
    }
    protected virtual void LeftStick(InputAction.CallbackContext value) { character.LeftStick(value); }
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