using UnityEngine;

public class RegisterInteraction : MonoBehaviour
{
    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Trigger the transition to first-person
            Debug.Log("Interacted with register!");
            // Call your camera/interaction switch here
            FindObjectOfType<CashierViewController>().EnterCashierMode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            // TODO: Show on-screen UI prompt (e.g. "Press E to begin order")
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            // TODO: Hide prompt
        }
    }
}
