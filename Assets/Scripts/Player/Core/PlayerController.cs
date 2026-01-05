using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInputReader inputReader;
    [SerializeField] private AnimationFSM animationFSM;
    [SerializeField] private SwordAttackController swordAttackController;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CharacterController characterController;

    [Header("Animator (Mesh Animator)")]
    [SerializeField] private Animator animator;   // Drag the Animator from your Mesh here

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 12f;

    [Header("Locomotion Blend Values")]
    [SerializeField] private float walkMoveValue = 0.5f;
    [SerializeField] private float runMoveValue = 1.0f;

    [Header("Gravity")]
    [SerializeField] private float gravity = -25f;
    [SerializeField] private float groundedStickForce = -2f;

    [Header("Sword Layer")]
    [SerializeField] private string swordLayerName = "Sword";
    [SerializeField] private float swordLayerFadeSpeed = 12f;
    [SerializeField] private float swordLayerActiveTime = 1.5f;

    [Header("Double Tap JumpOver")]
    [SerializeField] private float ctrlDoubleTapWindow = 0.25f;

    

    // cached per-frame
    private Vector2 moveInput;
    private bool isMoving;
    private bool isForward;
    private bool isRunning;
    private bool isCrouching;
    private float lastCtrlTapTime = -999f;


    // sword layer blend state
    private int swordLayerIndex = -1;
    private float swordLayerTimer;
    private bool swordLayerActive;

    // gravity state
    private float verticalVelocity;

    private void Awake()
    {
        if (inputReader == null)
            inputReader = GetComponent<PlayerInputReader>();

        if (animationFSM == null)
            animationFSM = GetComponent<AnimationFSM>();

        if (swordAttackController == null)
            swordAttackController = GetComponent<SwordAttackController>();

        if (characterController == null)
            characterController = GetComponent<CharacterController>();

        // Animator: prefer serialized reference, else try find in children
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.LogError("❌ PlayerController: Animator reference missing. Drag Mesh Animator into PlayerController.", this);
            return;
        }

        // Cache sword layer index once
        swordLayerIndex = animator.GetLayerIndex(swordLayerName);
        if (swordLayerIndex < 0)
            Debug.LogError($"❌ Sword layer '{swordLayerName}' not found in Animator.", this);

        // Inject references into attack controller (optional but safe)
        if (swordAttackController != null)
            swordAttackController.Initialize(inputReader, animationFSM);
    }

    private void Update()
    {
        if (inputReader == null || animationFSM == null) return;

        ReadInput();
        EvaluateMovementFlags();

        HandleLocomotion();
        HandleJump();
        HandleJumpOverDoubleTap();
        HandleRotation();
        HandleFrontFlip();
        HandleCrouch();

        HandleSwordAttackLayer();
        swordAttackController?.Tick();

        ApplyGravity();
    }

    private void ReadInput()
    {
        moveInput = inputReader.move_input;
    }

    private void EvaluateMovementFlags()
    {
        isMoving = moveInput.sqrMagnitude > 0.01f;
        isForward = moveInput.y > 0.1f;

        // Running blocked while crouching
        isRunning = !isCrouching && inputReader.sprint_held && isForward;
    }

    private void HandleLocomotion()
    {
        float moveParam = 0f;

        if (isMoving)
            moveParam = isRunning ? runMoveValue : walkMoveValue;

        animationFSM.UpdateLocomotion(moveParam);
    }

    private void HandleJump()
    {
        if (inputReader.jump_pressed)
            animationFSM.TriggerJump();
    }

    private void HandleJumpOverDoubleTap()
    {
        if (inputReader.jumpOverTap_pressed)
            animationFSM.TriggerJumpOver();
    }


    private void HandleRotation()
    {
        if (!isMoving || cameraTransform == null) return;

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 desiredDir = camForward * moveInput.y + camRight * moveInput.x;
        if (desiredDir.sqrMagnitude <= 0.001f) return;

        Quaternion targetRot = Quaternion.LookRotation(desiredDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
    }

    private void HandleFrontFlip()
    {
        if (inputReader.frontFlip_pressed)
            animationFSM.TriggerFrontFlip();
    }

    private void HandleCrouch()
    {
        if (!inputReader.crouch_pressed) return;

        isCrouching = !isCrouching;

        if (isCrouching)
            animationFSM.TriggerCrouch();
        else
            animationFSM.TriggerUnCrouch();
    }

    private void ApplyGravity()
    {
        if (characterController == null) return;

        if (characterController.isGrounded)
        {
            if (verticalVelocity < 0f)
                verticalVelocity = groundedStickForce;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }

    private void HandleSwordAttackLayer()
    {
        if (animator == null || swordLayerIndex < 0) return;

        // If attack button pressed → activate sword layer
        if (inputReader.attack_pressed)
        {
            swordLayerActive = true;
            swordLayerTimer = swordLayerActiveTime;
        }

        // Countdown while active
        if (swordLayerActive)
        {
            swordLayerTimer -= Time.deltaTime;
            if (swordLayerTimer <= 0f)
                swordLayerActive = false;
        }

        float currentWeight = animator.GetLayerWeight(swordLayerIndex);
        float targetWeight = swordLayerActive ? 1f : 0f;

        float newWeight = Mathf.MoveTowards(
            currentWeight,
            targetWeight,
            swordLayerFadeSpeed * Time.deltaTime
        );

        animator.SetLayerWeight(swordLayerIndex, newWeight);
    }
}
