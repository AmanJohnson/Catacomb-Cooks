using UnityEngine;
using System.Collections;

public class Punch : MonoBehaviour
{
    private Animator anim;
    private bool canCombo = false;
    private float comboTimeWindow = 3.0f; // Adjust as needed

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
            }

            canCombo = false; // Prevent further attacks
        }
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
