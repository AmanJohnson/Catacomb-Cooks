using UnityEngine;

public class PlayerRegisterTrigger : MonoBehaviour
{
    public CustomerRegisterTrigger customerZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("👤 Player entered E zone");

            if (customerZone != null)
                customerZone.SetPlayerInRange(true);
            else
                Debug.LogWarning("⚠️ customerZone not assigned!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (customerZone != null)
                customerZone.SetPlayerInRange(false);
        }
    }
}
