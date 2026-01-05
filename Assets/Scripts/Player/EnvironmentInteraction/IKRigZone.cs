using UnityEngine;

public class IKRigZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var controller = other.GetComponentInChildren<RigZoneWeightController>();
        if (controller != null)
            controller.EnterZone();
    }

    private void OnTriggerExit(Collider other)
    {
        var controller = other.GetComponentInChildren<RigZoneWeightController>();
        if (controller != null)
            controller.ExitZone();
    }
}
