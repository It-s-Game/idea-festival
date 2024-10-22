using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
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
    protected virtual void OnLeftStick(InputValue value) { Debug.Log("On Left Stick"); }
    protected virtual void OnRightStick(InputValue value) { Debug.Log("On Right Stick"); }
    protected virtual void OnButtonB(InputValue value) { Debug.Log("On Button B"); }
    protected virtual void OnButtonY(InputValue value) { Debug.Log("On Button Y"); }
    protected virtual void OnButtonA(InputValue value) { Debug.Log("On Button A"); }
    protected virtual void OnButtonX(InputValue value) { Debug.Log("On Button X"); }
    protected virtual void OnLeftBumper(InputValue value) { Debug.Log("On Left Bumper"); }
    protected virtual void OnRightBumper(InputValue value) { Debug.Log("On Right Bumper"); }
    protected virtual void OnLeftTrigger(InputValue value) { Debug.Log("On Left Trigger"); }
    protected virtual void OnRightTrigger(InputValue value) { Debug.Log("On Right Trigger"); }
    protected virtual void OnLeftStickPress(InputValue value) { Debug.Log("On Left Stick Press"); }
    protected virtual void OnRightStickPress(InputValue value) { Debug.Log("On Right Stick Press"); }
}