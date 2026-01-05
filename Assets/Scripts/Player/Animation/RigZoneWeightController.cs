using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigZoneWeightController : MonoBehaviour
{
    [Header("Rig Reference")]
    [SerializeField] private Rig rig;

    [Header("Blend")]
    [SerializeField] private float blendSpeed = 8f;

    [Header("Optional State Blockers")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private bool disableWhenAirborne = true;

    private int zoneCount = 0;     // supports overlapping zones
    private float targetWeight = 0f;

    private void Awake()
    {
        if (rig == null) rig = GetComponentInChildren<Rig>();
        if (characterController == null) characterController = GetComponent<CharacterController>();
        if (rig != null) rig.weight = 0f; // default off
    }

    private void Update()
    {
        if (rig == null) return;

        bool inZone = zoneCount > 0;

        bool stateAllowsIK = true;
        if (disableWhenAirborne && characterController != null)
            stateAllowsIK = characterController.isGrounded;

        targetWeight = (inZone && stateAllowsIK) ? 1f : 0f;

        rig.weight = Mathf.MoveTowards(rig.weight, targetWeight, blendSpeed * Time.deltaTime);
    }

    // Called by zone triggers:
    public void EnterZone() => zoneCount++;
    public void ExitZone() => zoneCount = Mathf.Max(0, zoneCount - 1);
}
