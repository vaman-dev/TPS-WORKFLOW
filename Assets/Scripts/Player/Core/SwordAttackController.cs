using UnityEngine;

public class SwordAttackController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInputReader inputReader;
    [SerializeField] private AnimationFSM animationFSM;

    [Header("Combo Settings")]
    [SerializeField] private float comboResetTime = 0.9f;   // time allowed between clicks
    [SerializeField] private float attackLockTime = 0.15f;  // prevents double trigger same frame

    private int comboIndex = 0;          // 0 -> next is 1
    private float lastAttackTime = -999f;
    private float nextAllowedAttackTime = 0f;

    /// <summary>
    /// Optional injection from PlayerController (prevents missing refs)
    /// </summary>
    public void Initialize(PlayerInputReader reader, AnimationFSM fsm)
    {
        inputReader = reader;
        animationFSM = fsm;
    }

    private void Awake()
    {
        // Fallback auto-fetch if not injected
        if (inputReader == null)
            inputReader = GetComponent<PlayerInputReader>();

        if (animationFSM == null)
            animationFSM = GetComponent<AnimationFSM>();
    }

    public void Tick()
    {
        if (inputReader == null || animationFSM == null)
            return;

        // Reset combo if player waited too long
        if (Time.time - lastAttackTime > comboResetTime)
            comboIndex = 0;

        // Only proceed on click
        if (!inputReader.attack_pressed)
            return;

        // Prevent double trigger spam
        if (Time.time < nextAllowedAttackTime)
            return;

        // Advance combo
        comboIndex++;
        if (comboIndex > 3) comboIndex = 1;

        // Trigger correct attack
        animationFSM.TriggerAttack(comboIndex);

        // Cache timing
        lastAttackTime = Time.time;
        nextAllowedAttackTime = Time.time + attackLockTime;
    }
}
