using UnityEngine;
using System.Collections;

/*
 * Punch.cs - Handles a simple combo attack system for a Unity Animator.
 *
 * Description:
 * - Plays a punch attack (`Punch_1`) when the player clicks.
 * - If the player clicks again within `comboTimeWindow`, a follow-up (`Hurricane`) is triggered.
 * - Uses a coroutine to wait a frame before checking animation state transitions.
 * - Prevents the combo from continuing past the allowed time window.
 *
 * Components:
 * - Requires an `Animator` component with `Punch_1` and `Hurricane` animations set up.
 * - Uses Layer 6 of the Animator (change if necessary).
 *
 * How It Works:
 * - Clicking (`Mouse0`) triggers `Punch_1` if an attack isn't active.
 * - Clicking again while `canCombo` is `true` triggers `Hurricane`.
 * - If no follow-up occurs before `comboTimeWindow` expires, the combo resets.
 * - Animator transitions should have "Has Exit Time" disabled for immediate triggering.
 *
 * Author: [Your Name or Team] | Date: [Optional]
 */

public class Punch : MonoBehaviour
{
    private Animator anim;
    private bool canCombo = false;
    private float comboTimeWindow = 2.0f; // Adjust as needed

    void Start() => anim = GetComponent<Animator>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(HandleAttack());
        }
    }

    IEnumerator HandleAttack()
    {
        yield return null; // Wait one frame for Animator to update

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(6); // Get current animation state

        if (!stateInfo.IsName("Hurricane") && !stateInfo.IsName("Punch_1") && !canCombo)
        {   
            Debug.Log("Punch_1 triggered!");
            anim.SetTrigger("Punch_1"); // Play first attack

            canCombo = true; // Allow second attack within time window

            CancelInvoke(nameof(ResetCombo)); // Prevent an old reset from interrupting
            Invoke(nameof(ResetCombo), comboTimeWindow);
        }  
        else if (canCombo)
        {
            Debug.Log("Hurricane triggered!");
            anim.SetTrigger("Hurricane"); // Play second attack

            yield return new WaitForSeconds(0.1f); // Allow Unity to process the trigger

            stateInfo = anim.GetCurrentAnimatorStateInfo(6); // Get updated state
            Debug.Log("Post-trigger check: IsName(Hurricane): " + stateInfo.IsName("Hurricane"));

            if (stateInfo.IsName("Hurricane"))
            {
                Debug.Log("Confirmed Hurricane is playing!");
            }
            else
            {
                Debug.LogError("ERROR: Hurricane did not start properly!");
                 ForceResetCombo(); // 🔴 🔥 Failed to trigger for some reason. RESET COMBO
            }

            canCombo = false; // Prevent further attacks
        }
    }

    void ForceResetCombo()
{
    Debug.Log("Force Reset: Restarting combo system completely.");

    canCombo = false; // Make sure next attack is always Punch_1
    anim.ResetTrigger("Punch_1"); // Clear any old triggers
    anim.ResetTrigger("Hurricane");

    CancelInvoke(nameof(ResetCombo)); // Stop any pending resets
}


    void ResetCombo()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(6);

        Debug.Log("stateInfo In Reset: IsName(Punch_1): " + stateInfo.IsName("Punch_1") 
            + " | IsName(Hurricane): " + stateInfo.IsName("Hurricane"));

        if (!stateInfo.IsName("Punch_1") && !stateInfo.IsName("Hurricane")) // Only reset if not attacking
        {
            Debug.Log("Combo window expired. Resetting combo.");
            canCombo = false;
        }
    }
}
