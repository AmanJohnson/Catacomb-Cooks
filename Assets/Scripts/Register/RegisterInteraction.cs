using UnityEngine;

public class RegisterInteraction : MonoBehaviour
{
    private CustomerAI currentCustomer;
    private bool isCustomerInRange = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressed E | CustomerInRange: " + isCustomerInRange);
        }

        if (isCustomerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interacted with register (customer zone)!");

            if (currentCustomer != null)
            {
                FindObjectOfType<CashierViewController>().EnterCashierMode(currentCustomer.profile);
            }
            else
            {
                Debug.LogWarning("⚠️ No customer detected at register.");
            }
        }
    }
void OnTriggerStay(Collider other)
{
    Debug.Log("🌀 Still inside trigger: " + other.name);
}

private void OnTriggerEnter(Collider other)
{
Debug.Log("▶️ OnTriggerEnter FIRED by: " + other.name + " | Tag: " + other.tag + " | Layer: " + other.gameObject.layer);
  Debug.Log("🧠 TRIGGERED BY: " + other.name);
    Debug.Log("🧠 PARENT: " + other.transform.root.name);
    Debug.Log("🧠 Has CustomerAI: " + (other.GetComponentInParent<CustomerAI>() != null));
var ai = other.GetComponentInParent<CustomerAI>();
if (ai != null)
{
    Debug.Log("✅ SUCCESS: Found CustomerAI on parent object: " + ai.name);
    isCustomerInRange = true;
    currentCustomer = ai;
}
else
{
    Debug.Log("❌ FAIL: Still no CustomerAI found in parent of: " + other.name);
}

}



 private void OnTriggerExit(Collider other)
{
    Debug.Log("🚪 Trigger Exited by: " + other.name + " | Tag: " + other.tag);

    var ai = other.GetComponentInParent<CustomerAI>();
    if (ai != null)
    {
        Debug.Log("🚪 Customer exited: " + other.name);
        isCustomerInRange = false;
        currentCustomer = null;
    }
}

}
