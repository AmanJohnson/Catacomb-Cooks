using UnityEngine;

public class ShowFloatingOnProximity : MonoBehaviour
{
    public GameObject floatingE;              // Assign this in Inspector
    public Transform playerTransform;         // Assign this in Inspector
    public float triggerDistance = 3.5f;      // Show E if within this distance
    public float refreshDelay = 0.2f;         // Small cooldown to prevent spam

    private bool isVisible = false;
    private float lastToggleTime = 0f;

    void Update()
    {
        if (playerTransform == null || floatingE == null) return;

        float distance = Vector3.Distance(playerTransform.position, transform.position);
        float timeSinceToggle = Time.time - lastToggleTime;

        bool shouldShow = distance < triggerDistance;

        if (shouldShow != isVisible && timeSinceToggle > refreshDelay)
        {
            floatingE.SetActive(shouldShow);
            isVisible = shouldShow;
            lastToggleTime = Time.time;

            Debug.Log($"🔁 FloatingE toggled: {shouldShow} (distance: {distance})");
        }
    }
}
