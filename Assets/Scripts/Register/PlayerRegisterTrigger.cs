using UnityEngine;

public class PlayerRegisterTrigger : MonoBehaviour
{
    public CustomerRegisterTrigger customerZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("👤 Player entered E zone");
            customerZone.SetPlayerInRange(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("👤 Player left E zone");
            customerZone.SetPlayerInRange(false);
        }
    }
}
