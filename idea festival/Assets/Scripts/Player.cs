using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [HideInInspector]
    public PlayerInput playerInput;

    public Controller controller;

    private InputAction leftStick = null;

    private int playerIndex;

    public Controller Controller { set { controller = value; } }
    public int PlayerIndex { get { return playerIndex; } }
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        playerIndex = playerInput.playerIndex;

        Managers.Instance.players.Add(this);

        DontDestroyOnLoad(gameObject);
    }
    public void Init(Controller character, Vector2 position)
    {
        controller = character;
        
        leftStick = playerInput.actions["LeftStick"];

        Set(position);
    }
    private void Set(Vector2 position)
    {
        controller.Set(leftStick, position);

        leftStick.started += LeftStickAction;
        leftStick.canceled += LeftStickAction;
    }
    public void Resetting()
    {
        leftStick.started -= LeftStickAction;
        leftStick.canceled -= LeftStickAction;

        controller = null;
    }
    private void LeftStickAction(InputAction.CallbackContext context)
    {
        controller.LeftStick();
    }
    private void OnButtonY(InputValue value)
    {
        if(Managers.Instance.isInGame)
        {
            controller.ButtonY(value);
        }
    }
    private void OnButtonX(InputValue value)
    {
        if (Managers.Instance.isInGame)
        {
            controller.ButtonX(value);
        }
    }
    private void OnButtonA(InputValue value)
    {
        if (Managers.Instance.isInGame)
        {
            controller.ButtonA(value);
        }
    }
    private void OnButtonB(InputValue value)
    {
        if (Managers.Instance.isInGame)
        {
            controller.ButtonB(value);
        }
        else
        {
            Managers.UI.DisableUI();
        }
    }
    private void OnLeftBumper(InputValue value)
    {
        if (Managers.Instance.isInGame)
        {
            controller.LeftBumper(value);
        }
    }
    private void OnRightBumper(InputValue value)
    {
        if (Managers.Instance.isInGame)
        {
            controller.RightBumper(value);
        }
    }
    private void OnLeftTrigger(InputValue value)
    {
        if (Managers.Instance.isInGame)
        {
            controller.LeftTrigger(value);
        }
    }
    private void OnRightTrigger(InputValue value)
    {
        if (Managers.Instance.isInGame)
        {
            controller.RightTrigger(value);
        }
    }
    private void OnLeftStickPress(InputValue value)
    {
        if (Managers.Instance.isInGame)
        {
            controller.LeftStickPress(value);
        }
    }
    private void OnRightStickPress(InputValue value)
    {
        if (Managers.Instance.isInGame)
        {
            controller.RightStickPress(value);
        }
    }
}