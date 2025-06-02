using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform spawnPoint;
    public Transform registerTarget;

    void Start()
    {
        GameObject customer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);

        // Assign the register target at runtime
        CustomerAI ai = customer.GetComponent<CustomerAI>();
        if (ai != null && registerTarget != null)
        {
            ai.SetRegisterTarget(registerTarget);
        }
    }
}

