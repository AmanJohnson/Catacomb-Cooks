using UnityEngine;

public class CustomerRegisterTrigger : MonoBehaviour
{
    private CustomerAI currentCustomer;
    private bool isCustomerInRange = false;
    private bool isPlayerInRange = false;

  void Update()
{
 if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
{
    Debug.Log("✅ Interacted with register!");

    var controller = FindObjectOfType<CashierViewController>();

    if (currentCustomer != null)
    {
        Debug.Log("📦 currentCustomer exists");

        if (currentCustomer.profile != null)
        {
            Debug.Log("💬 Profile is valid: " + currentCustomer.profile.customerName);
            controller.EnterCashierMode(currentCustomer.profile);
        }
        else
        {
            Debug.LogWarning("⚠️ Customer has no profile — still entering cashier mode");
            controller.EnterCashierMode(null);
        }
    }
    else
    {
        Debug.LogWarning("⚠️ No customer in front — entering cashier mode without dialogue");
        controller.EnterCashierMode(null);
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
