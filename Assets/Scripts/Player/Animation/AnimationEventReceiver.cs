using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{
    [SerializeField] private GameObject sword;

    public void ShowSword()
    {
        sword.SetActive(true);
    }

    public void HideSword()
    {
        sword.SetActive(false);
    }
}
