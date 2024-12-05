using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    private InputAction leftStick = null;

    private CharacterController character;
    private PlayerInput playerInput;

    public CharacterController Character { set { character = value; } }
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    public void Init(CharacterController character)
    {
        this.character = character;

        leftStick = playerInput.actions["LeftStick"];
    }
    public void Set()
    {
        leftStick.started += (ctx =>
        {
            character.LeftStick(ctx);
        });
        leftStick.canceled += (ctx =>
        {
            character.LeftStick(ctx);
        });
    }
    private void OnButtonY(InputValue value)
    {
        character.ButtonY(value);
    }
    private void OnButtonX(InputValue value)
    {
        character.ButtonX(value);
    }
    private void OnButtonA(InputValue value)
    {
        character.ButtonA(value);
    }
    private void OnButtonB(InputValue value)
    {
        character.ButtonB(value);
    }
    private void OnLeftBumper(InputValue value)
    {
        character.LeftBumper(value);
    }
    private void OnRightBumper(InputValue value)
    {
        character.RightBumper(value);
    }
    private void OnLeftTrigger(InputValue value)
    {
        character.LeftTrigger(value);
    }
    private void OnRightTrigger(InputValue value)
    {
        character.RightTrigger(value);
    }
    private void OnLeftStickPress(InputValue value)
    {
        character.LeftStickPress(value);
    }
    private void OnRightStickPress(InputValue value)
    {
        character.RightStickPress(value);
    }
}