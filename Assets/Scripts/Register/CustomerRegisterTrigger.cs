using UnityEngine;

public class CustomerRegisterTrigger : MonoBehaviour
{
    private CustomerAI currentCustomer;
    private bool isCustomerInRange = false;
    private bool isPlayerInRange = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"Pressed E | CustomerInRange: {isCustomerInRange} | PlayerInRange: {isPlayerInRange}");
        }

        if (isCustomerInRange && isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("✅ Interacted with register!");

            if (currentCustomer != null)
            {
                FindObjectOfType<CashierViewController>().EnterCashierMode(currentCustomer.profile);
            }
            else
            {
                Debug.LogWarning("⚠️ No customer assigned");
            }
        }
    }



    public void SetPlayerInRange(bool state)
    {
        Debug.Log("📡 SetPlayerInRange called with: " + state);
        isPlayerInRange = state;
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("📦 Trigger hit by: " + other.name);

        var ai = other.GetComponentInParent<CustomerAI>();
        if (ai != null)
        {
            Debug.Log("✅ Found CustomerAI: " + ai.name);
            isCustomerInRange = true;
            currentCustomer = ai;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var ai = other.GetComponentInParent<CustomerAI>();
        if (ai != null)
        {
            Debug.Log("🚪 Customer exited: " + ai.name);
            isCustomerInRange = false;
            currentCustomer = null;
        }
    }
}
