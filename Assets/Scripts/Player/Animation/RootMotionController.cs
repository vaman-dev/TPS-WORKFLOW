using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RootMotionController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController characterController;

    [SerializeField] private bool useRootMotion = true;

    private void Reset()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnAnimatorMove()
    {
        if (!useRootMotion || animator == null || characterController == null)
            return;

        Vector3 delta = animator.deltaPosition;

        // 🔍 Debug log the delta being generated & applied
        Debug.Log($"[RootMotion] delta = {delta} | magnitude = {delta.magnitude}");

        // Apply the delta to move the CharacterController
        characterController.Move(delta);

        // Optional rotation
        Quaternion rot = animator.rootRotation;
        transform.rotation = Quaternion.Euler(0f, rot.eulerAngles.y, 0f);
    }


    public void SetRootMotion(bool enabled)
    {
        useRootMotion = enabled;
    }
}
