using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    private InputAction leftStick = null;

    [SerializeField]//
    private CharacterController character;
    private PlayerInput playerInput;

    private int playerIndex;

    public CharacterController Character { set { character = value; } }
    public int PlayerIndex { get { return playerIndex; } }
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        playerIndex = playerInput.playerIndex;
    }
    private void Update()//
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Init(character);
        }
    }
    public void Init(CharacterController character)
    {
        this.character = character;
        
        leftStick = playerInput.actions["LeftStick"];

        Set();
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

        character.Set(playerIndex);
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