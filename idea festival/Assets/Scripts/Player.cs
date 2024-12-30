using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [HideInInspector]
    public PlayerInput input;

    public Controller controller;

    private InputAction leftStick = null;

    private int playerIndex;

    public Controller Controller { set { controller = value; } }
    public int PlayerIndex { get { return playerIndex; } }
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        playerIndex = input.playerIndex;

        Managers.Game.playerCount++;

        Managers.Instance.players.Add(this);

        InputSystem.onDeviceChange += OnDeviceChange;

        DontDestroyOnLoad(gameObject);
    }

    public void OnDeviceChange(InputDevice targetDevice, InputDeviceChange change)
    {
        if(controller != null)
        {
            return;
        }

        Managers.Game.playerCount--;

        if (change == InputDeviceChange.Removed)
        {
            foreach(Player player in Managers.Instance.players)
            {
                if(player.input.devices.Count == 0)
                {
                    Managers.Instance.players.Remove(player);

                    Destroy(player.gameObject);

                    break;
                }
            }
        }
    }
    public void Init(Controller character)
    {
        controller = character;
        
        leftStick = input.actions["LeftStick"];

        Set();
    }
    private void Set()
    {
        controller.Set(leftStick, playerIndex);

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
        controller?.LeftStick();
    }
    private void OnButtonY(InputValue value)
    {
        controller?.ButtonY(value);
    }
    private void OnButtonX(InputValue value)
    {
        controller?.ButtonX(value);
    }
    private void OnButtonA(InputValue value)
    {
        controller?.ButtonA(value);
    }
    private void OnButtonB(InputValue value)
    {
        controller?.ButtonB(value);
    }
    private void OnLeftBumper(InputValue value)
    {
        controller?.LeftBumper(value);
    }
    private void OnRightBumper(InputValue value)
    {
        controller?.RightBumper(value);
    }
    private void OnLeftTrigger(InputValue value)
    {
        controller?.LeftTrigger(value);
    }
    private void OnRightTrigger(InputValue value)
    {
        controller?.RightTrigger(value);
    }
    private void OnLeftStickPress(InputValue value)
    {
        controller?.LeftStickPress(value);
    }
    private void OnRightStickPress(InputValue value)
    {
        controller?.RightStickPress(value);
    }
}