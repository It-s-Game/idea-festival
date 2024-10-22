using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class CharacterController : MonoBehaviour
{
    protected Character character;

    private PlayerInput playerInput;

    private int playerIndex;

    public int PlayerIndex { get { return playerIndex; } }
    protected virtual void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerInput.actions == null)
        {
            playerInput.actions = Resources.Load<InputActionAsset>("PlayerController");
        }

        playerInput.actions.Enable();
    }
    protected virtual void Start()
    {
        playerIndex = playerInput.playerIndex;
    }
    protected virtual void OnLeftStick(InputValue context) { }
    protected virtual void OnRightStick(InputValue context) { }
    protected virtual void OnButtonB(InputValue context) { }
    protected virtual void OnButtonY(InputValue context) { }
    protected virtual void OnButtonA(InputValue context) { }
    protected virtual void OnButtonX(InputValue context) { }
    protected virtual void OnLeftBumper(InputValue context) { }
    protected virtual void OnRightBumper(InputValue context) { }
    protected virtual void OnLeftTrigger(InputValue context) { }
    protected virtual void OnRightTrigger(InputValue context) { }
    protected virtual void OnLeftStickPress(InputValue context) { }
    protected virtual void OnRightStickPress(InputValue context) { }
}