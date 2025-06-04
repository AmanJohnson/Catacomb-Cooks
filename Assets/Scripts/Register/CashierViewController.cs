using UnityEngine;
using Invector.vCharacterController;

public class CashierViewController : MonoBehaviour
{
    public Camera thirdPersonCamera;
    public Camera cashierCamera;
    public vThirdPersonController playerController;

    public DialogueSystem dialogueSystem;

    private bool isInCashierMode = false;


    private SkinnedMeshRenderer[] cachedRenderers;
    
    public GameObject inventoryCanvas; 


private void CacheRenderersIfNeeded()
    {
        if (cachedRenderers == null || cachedRenderers.Length == 0)
        {
            cachedRenderers = GetComponentsInChildren<SkinnedMeshRenderer>(true);
            Debug.Log("🔍 Found " + cachedRenderers.Length + " SkinnedMeshRenderers.");
            foreach (var r in cachedRenderers)
            {
                Debug.Log("🎯 Renderer: " + r.gameObject.name);
            }
        }
    }



    public void EnterCashierMode(CustomerProfile customerProfile)
    {
        if (isInCashierMode) return;

        Debug.Log(">>> EnterCashierMode triggered!");

        if (inventoryCanvas != null)
        inventoryCanvas.SetActive(false);


        // Disable player control
        if (playerController != null)
        {
            playerController.lockMovement = true;
            playerController.lockRotation = true;
            playerController.input = Vector2.zero;

            Rigidbody rb = playerController.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true; // 💥 Freeze body entirely
            }
        }

        // Switch cameras
        if (thirdPersonCamera != null)
        {
            thirdPersonCamera.gameObject.SetActive(false);
            Debug.Log("Third-person camera GameObject disabled.");
        }

        if (cashierCamera != null)
        {
            cashierCamera.gameObject.SetActive(true);
            Debug.Log("Cashier camera GameObject enabled.");
        }

        // Unlock cursor for UI
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Cursor unlocked and visible.");

        // Hide player visuals (VRM)
        CacheRenderersIfNeeded();
        foreach (var r in cachedRenderers)
        {
            r.enabled = false;
        }


        isInCashierMode = true;

    
    if (dialogueSystem != null && customerProfile != null)
    {
            // New method we'll define in DialogueSystem to accept a whole profile
        Debug.Log("🟢 Cashier mode triggered: about to start dialogue");

        dialogueSystem.StartCustomerInteraction(customerProfile);
    }

}

    public void ExitCashierMode()
    {
        if (!isInCashierMode) return;

        Debug.Log(">>> ExitCashierMode triggered!");


    if (inventoryCanvas != null)
        inventoryCanvas.SetActive(true);

        // Re-enable player control
        if (playerController != null)
        {
            playerController.lockMovement = false;
            playerController.lockRotation = false;
        }

        // Switch cameras back (Reactivate third person camera object)

                // Re-enable player visuals
        foreach (var r in cachedRenderers)
        {
            r.enabled = true;
        }

        if (cashierCamera != null)
        {
            cashierCamera.gameObject.SetActive(false);
            Debug.Log("Cashier camera GameObject disabled.");
        }

        if (thirdPersonCamera != null)
        {
            thirdPersonCamera.gameObject.SetActive(true);
            Debug.Log("Third-person camera GameObject enabled.");
        }

        // Lock cursor back for gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("Cursor locked and hidden.");

        Rigidbody rb = playerController.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }


        isInCashierMode = false;
    }
}
