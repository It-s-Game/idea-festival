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
        character.OnButtonY(value);
    }
    private void OnButtonX(InputValue value)
    {
        character.OnButtonX(value);
    }
    private void OnButtonA(InputValue value)
    {
        character.OnButtonA(value);
    }
    private void OnButtonB(InputValue value)
    {
        character.OnButtonB(value);
    }
    private void OnLeftBumper(InputValue value)
    {
        character.OnLeftBumper(value);
    }
    private void OnRightBumper(InputValue value)
    {
        character.OnRightBumper(value);
    }
    private void OnLeftTrigger(InputValue value)
    {
        character.OnLeftTrigger(value);
    }
    private void OnRightTrigger(InputValue value)
    {
        character.OnRightTrigger(value);
    }
    private void OnLeftStickPress(InputValue value)
    {
        character.OnLeftStickPress(value);
    }
    private void OnRightStickPress(InputValue value)
    {
        character.RightStickPress(value);
    }
}