using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    [SerializeField] private InputSystem_Actions inputActions;

    [Header("Debug")]
    [SerializeField] private bool logInputs = false;

    public Vector2 move_input { get; private set; }
    public Vector2 look_input { get; private set; }

    public bool sprint_held { get; private set; }

    public bool jump_pressed { get; private set; }
    public bool frontFlip_pressed { get; private set; }
    public bool crouch_pressed { get; private set; }
    public bool jumpOverTap_pressed { get; private set; }

    // Attack (LMB one-frame press)
    public bool attack_pressed { get; private set; }

    private void Awake()
    {
        if (inputActions == null)
            inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Enable();

        if (inputActions.Player.Move == null) { Debug.LogError("❌ Action 'Move' not found in Action Map 'Player'."); return; }
        if (inputActions.Player.Look == null) { Debug.LogError("❌ Action 'Look' not found in Action Map 'Player'."); return; }
        if (inputActions.Player.Sprint == null) { Debug.LogError("❌ Action 'Sprint' not found in Action Map 'Player'."); return; }
        if (inputActions.Player.Jump == null) { Debug.LogError("❌ Action 'Jump' not found in Action Map 'Player'."); return; }
        if (inputActions.Player.FrontFlip == null) { Debug.LogError("❌ Action 'FrontFlip' not found in Action Map 'Player'."); return; }
        if (inputActions.Player.Crouch == null) { Debug.LogError("❌ Action 'Crouch' not found in Action Map 'Player'."); return; }
        if (inputActions.Player.Attack == null) { Debug.LogError("❌ Action 'Attack' not found in Action Map 'Player'. (Bind LMB)"); return; }
        if (inputActions.Player.JumpOverTap == null) { Debug.LogError("❌ Action 'JumpOverTap' not found in Action Map 'Player'."); return; }

        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;

        inputActions.Player.Look.performed += OnLook;
        inputActions.Player.Look.canceled += OnLook;

        inputActions.Player.Sprint.performed += OnSprint;
        inputActions.Player.Sprint.canceled += OnSprint;

        inputActions.Player.Jump.performed += OnJump;
        inputActions.Player.JumpOverTap.performed += OnJumpOverTap;

        inputActions.Player.FrontFlip.performed += OnFrontFlip;
        inputActions.Player.Crouch.performed += OnCrouch;

        // Attack (LMB)
        inputActions.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        if (inputActions == null) return;

        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;

        inputActions.Player.Look.performed -= OnLook;
        inputActions.Player.Look.canceled -= OnLook;

        inputActions.Player.Sprint.performed -= OnSprint;
        inputActions.Player.Sprint.canceled -= OnSprint;

        inputActions.Player.Jump.performed -= OnJump;
        inputActions.Player.JumpOverTap.performed -= OnJumpOverTap;

        inputActions.Player.FrontFlip.performed -= OnFrontFlip;
        inputActions.Player.Crouch.performed -= OnCrouch;

        inputActions.Player.Attack.performed -= OnAttack;

        inputActions.Disable();
    }

    private void LateUpdate()
    {
        jump_pressed = false;
        frontFlip_pressed = false;
        crouch_pressed = false;
        attack_pressed = false;
        jumpOverTap_pressed = false;
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        move_input = ctx.ReadValue<Vector2>();
        if (logInputs) Debug.Log("Move = " + move_input);
    }

    private void OnLook(InputAction.CallbackContext ctx)
    {
        look_input = ctx.ReadValue<Vector2>();
        if (logInputs) Debug.Log("Look = " + look_input);
    }

    private void OnSprint(InputAction.CallbackContext ctx)
    {
        sprint_held = ctx.ReadValueAsButton();
        if (logInputs) Debug.Log("Sprint Held = " + sprint_held);
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        jump_pressed = true;
        if (logInputs) Debug.Log("Jump Pressed");
    }

    private void OnFrontFlip(InputAction.CallbackContext ctx)
    {
        frontFlip_pressed = true;
        if (logInputs) Debug.Log("Front Flip Pressed");
    }

    private void OnCrouch(InputAction.CallbackContext ctx)
    {
        crouch_pressed = true;
        if (logInputs) Debug.Log("Crouch Pressed");
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        attack_pressed = true;
        if (logInputs) Debug.Log("Attack Pressed (LMB)");
    }

    private void OnJumpOverTap(InputAction.CallbackContext ctx)
    {
        jumpOverTap_pressed = true;
        if (logInputs) Debug.Log("Tab Tap Pressed");
    }
}
