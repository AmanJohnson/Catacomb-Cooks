using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    private Transform registerTarget;
    public float moveSpeed = 1.5f;
    private bool isWalking = true;

    public CustomerProfile profile;

    private Animator animator;
    private Vector3 lastPosition;

    public Transform positionReference;

    public bool hasStartedDialogue = false;

    private bool hasReachedRegister = false;


    void Start()
    {
        animator = GetComponent<Animator>();
        if (positionReference != null)
        {
            lastPosition = positionReference.position;
        }
    }

    public void SetRegisterTarget(Transform target)
    {
        registerTarget = target;
    }

    void Update()
    {
        // Debug draw + log (optional)
        if (registerTarget != null)
        {
            Vector3 customerXZ = new Vector3(positionReference.position.x, 0f, positionReference.position.z);
            Vector3 registerXZ = new Vector3(registerTarget.position.x, 0f, registerTarget.position.z);
            float distance = Vector3.Distance(customerXZ, registerXZ);


            // Movement logic
            if (isWalking)
            {
                Vector3 targetPosition = new Vector3(
                    registerTarget.position.x,
                    transform.position.y, // keep Y locked
                    registerTarget.position.z
                );

                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPosition,
                    moveSpeed * Time.deltaTime
                );

               if (distance < 0.5f)
{
    isWalking = false;

    if (!hasReachedRegister)
    {
        hasReachedRegister = true;

        CashierViewController cashier = FindObjectOfType<CashierViewController>();
        if (cashier != null)
        {
            Debug.Log("🧍 Customer reached register — setting currentCustomer");
            cashier.currentCustomer = this;
        }
    }
}

            }
        }

        // Animation speed logic
        float currentSpeed = (positionReference.position - lastPosition).magnitude / Time.deltaTime;
        if (animator != null)
        {
            animator.SetFloat("speed", isWalking ? currentSpeed : 0f);
        }

        lastPosition = positionReference.position;

    }
    
    public bool IsWalking()
{
    return isWalking;
}


}
