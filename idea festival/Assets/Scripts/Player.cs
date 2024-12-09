using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    private InputAction leftStick = null;

    [SerializeField]//
    private Controller controller;
    private PlayerInput playerInput;

    private int playerIndex;

    public Controller Character { set { controller = value; } }
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
            Init(controller);
        }
    }
    public void Init(Controller character)
    {
        this.controller = character;
        
        leftStick = playerInput.actions["LeftStick"];

        Set();
    }
    public void Set()
    {
        controller.Set(leftStick, playerIndex);

        leftStick.started += (ctx =>
        {
            controller.LeftStick(ctx);
        });
        leftStick.canceled += (ctx =>
        {
            controller.LeftStick(ctx);
        });
    }
    private void OnButtonY(InputValue value)
    {
        controller.ButtonY(value);
    }
    private void OnButtonX(InputValue value)
    {
        controller.ButtonX(value);
    }
    private void OnButtonA(InputValue value)
    {
        controller.ButtonA(value);
    }
    private void OnButtonB(InputValue value)
    {
        controller.ButtonB(value);
    }
    private void OnLeftBumper(InputValue value)
    {
        controller.LeftBumper(value);
    }
    private void OnRightBumper(InputValue value)
    {
        controller.RightBumper(value);
    }
    private void OnLeftTrigger(InputValue value)
    {
        controller.LeftTrigger(value);
    }
    private void OnRightTrigger(InputValue value)
    {
        controller.RightTrigger(value);
    }
    private void OnLeftStickPress(InputValue value)
    {
        controller.LeftStickPress(value);
    }
    private void OnRightStickPress(InputValue value)
    {
        controller.RightStickPress(value);
    }
}