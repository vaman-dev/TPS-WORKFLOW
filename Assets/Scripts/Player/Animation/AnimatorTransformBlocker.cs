using UnityEngine;

public class AnimatorTransformBlocker : MonoBehaviour
{
    private void OnAnimatorMove()
    {
        // Stop animator from moving its own transform
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
