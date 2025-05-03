using UnityEngine;
using Invector.vCharacterController;

public class CashierViewController : MonoBehaviour
{
    public Camera thirdPersonCamera;
    public Camera cashierCamera;
    public vThirdPersonController playerController;

    private bool isInCashierMode = false;

    public void EnterCashierMode()
    {
        if (isInCashierMode) return;

        Debug.Log(">>> EnterCashierMode triggered!");

        // Disable player control
       if (playerController != null)
        {
            playerController.lockMovement = true;
            playerController.lockRotation = true;
            playerController.input = Vector2.zero; // 🚫 Stop any current motion
        }


        // Switch cameras (Deactivate third person camera object)
        if (thirdPersonCamera != null)
        {
            thirdPersonCamera.gameObject.SetActive(false);
            Debug.Log("Third-person camera GameObject disabled.");
        }

        // Activate cashier camera object
        if (cashierCamera != null)
        {
            cashierCamera.gameObject.SetActive(true);
            Debug.Log("Cashier camera GameObject enabled.");
        }

        // Unlock cursor for UI
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Cursor unlocked and visible.");

        isInCashierMode = true;
    }

    public void ExitCashierMode()
    {
        if (!isInCashierMode) return;

        Debug.Log(">>> ExitCashierMode triggered!");

        // Re-enable player control
        if (playerController != null)
        {
            playerController.lockMovement = false;
            playerController.lockRotation = false;
        }

        // Switch cameras back (Reactivate third person camera object)
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

        isInCashierMode = false;
    }
}
