using UnityEngine;

public class AnimationFSM : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;

    [Header("Locomotion Settings")]
    [SerializeField] private float moveDampTime = 0.1f;

    // ---------------- PARAMETERS ----------------

    // Locomotion
    private static readonly int Move_Hash = Animator.StringToHash("Move");

    // Base actions
    private static readonly int Jump_Hash = Animator.StringToHash("Jump");
    private static readonly int FrontFlip_Hash = Animator.StringToHash("FrontFlip");
    private static readonly int Crouch_Hash = Animator.StringToHash("Crouch");
    private static readonly int UnCrouch_Hash = Animator.StringToHash("UnCrouch");

    // NEW: JumpOver (double Ctrl)
    private static readonly int JumpOver_Hash = Animator.StringToHash("JumpOver");


    // Sword Attacks
    private static readonly int Attack1_Hash = Animator.StringToHash("Attack1");
    private static readonly int Attack2_Hash = Animator.StringToHash("Attack2");
    private static readonly int Attack3_Hash = Animator.StringToHash("Attack3");

    private void Reset()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    // ---------------- LOCOMOTION ----------------
    public void UpdateLocomotion(float moveValue)
    {
        if (animator == null) return;

        animator.SetFloat(Move_Hash, moveValue, moveDampTime, Time.deltaTime);
    }

    // ---------------- BASIC ACTIONS ----------------
    public void TriggerJump()
    {
        if (animator == null) return;

        animator.ResetTrigger(Jump_Hash);
        animator.SetTrigger(Jump_Hash);
    }

    public void TriggerFrontFlip()
    {
        if (animator == null) return;

        animator.ResetTrigger(FrontFlip_Hash);
        animator.SetTrigger(FrontFlip_Hash);
    }

    public void TriggerCrouch()
    {
        if (animator == null) return;

        animator.ResetTrigger(UnCrouch_Hash);
        animator.SetTrigger(Crouch_Hash);
    }

    public void TriggerUnCrouch()
    {
        if (animator == null) return;

        animator.ResetTrigger(Crouch_Hash);
        animator.SetTrigger(UnCrouch_Hash);
    }

 
    public void TriggerJumpOver()
    {
        if (animator == null) return;

        animator.ResetTrigger(JumpOver_Hash);
        animator.SetTrigger(JumpOver_Hash);
    }

    // ---------------- SWORD ATTACKS ----------------
    public void TriggerAttack1()
    {
        if (animator == null) return;

        animator.ResetTrigger(Attack1_Hash);
        animator.SetTrigger(Attack1_Hash);
    }

    public void TriggerAttack2()
    {
        if (animator == null) return;

        animator.ResetTrigger(Attack2_Hash);
        animator.SetTrigger(Attack2_Hash);
    }

    public void TriggerAttack3()
    {
        if (animator == null) return;

        animator.ResetTrigger(Attack3_Hash);
        animator.SetTrigger(Attack3_Hash);
    }

    /// <summary>
    /// Combo helper: pass 1,2,3
    /// </summary>
    public void TriggerAttack(int comboIndex)
    {
        if (animator == null) return;

        switch (comboIndex)
        {
            case 1: TriggerAttack1(); break;
            case 2: TriggerAttack2(); break;
            case 3: TriggerAttack3(); break;
            default: TriggerAttack1(); break;
        }
    }
}
