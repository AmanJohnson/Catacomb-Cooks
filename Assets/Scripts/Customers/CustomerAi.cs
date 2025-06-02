using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    private Transform registerTarget;
    public float moveSpeed = 1.5f;
    private bool isWalking = true;

    public CustomerProfile profile;


    public void SetRegisterTarget(Transform target)
    {
        registerTarget = target;
    }

    void Update()
    {
        if (isWalking && registerTarget != null)
        {
            // Only move on the XZ plane (no drifting due to Y axis mismatch)
            Vector3 targetPosition = new Vector3(
                registerTarget.position.x,
                transform.position.y, // Keep customer's current Y
                registerTarget.position.z
            );

            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                isWalking = false;
                Debug.Log("✅ Customer reached the register exactly.");
            }
        }
    }
}
