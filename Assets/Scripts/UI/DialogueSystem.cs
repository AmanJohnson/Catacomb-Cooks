using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;

    [TextArea]
    public string[] dialogueLines;

    private int currentLine = 0;
    private bool isDialogueActive = false;

    private CustomerProfile currentCustomerProfile;

    public GameObject responsePanel; // assign in inspector
    public Button[] responseButtons; // assign 3 in inspector
    private State currentState;

    private bool readyToExit = false;

    private enum State { Intro, Order, Choice }

    public TipManager tipManager; // Assign in inspector


void Start()
{
    if (dialoguePanel == null)
    {
        Debug.LogError("❌ dialoguePanel is NULL in Start()");
    }
    else
    {
        dialoguePanel.SetActive(false);
        Debug.Log("▶️ dialoguePanel assigned to: " + dialoguePanel.name);
    }

    isDialogueActive = false;

    if (nextButton != null)
    {
        nextButton.onClick.AddListener(AdvanceDialogue);
    }
    else
    {
        Debug.LogWarning("❌ nextButton is null in Start()");
    }
}


    void Update()
    {
        // Only allow dialogue advancing during Intro and Order
        if (isDialogueActive &&
            currentState != State.Choice &&
            (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)))
        {
            AdvanceDialogue();
        }
    
    if (readyToExit &&
    (Input.GetKeyDown(KeyCode.Return) ||
     Input.GetKeyDown(KeyCode.Space) ||
     Input.GetKeyDown(KeyCode.Escape) ||
     Input.GetKeyDown(KeyCode.Backspace)))
{
    Debug.Log("🧹 Ending dialogue and exiting cashier mode");
    
    EndDialogue(); // <-- hides the dialogue UI
    FindObjectOfType<CashierViewController>().ExitCashierMode();
    
    readyToExit = false;
}


}

public void StartCustomerInteraction(CustomerProfile profile)
{
    if (profile == null)
    {
        Debug.LogError("❌ StartCustomerInteraction() called with NULL profile");
        return;
    }

    if (dialoguePanel == null)
    {
        Debug.LogError("❌ dialoguePanel is NULL in StartCustomerInteraction()");
        return;
    }

    if (dialogueText == null)
    {
        Debug.LogError("❌ dialogueText is NULL in StartCustomerInteraction()");
        return;
    }

    Debug.Log("💬 StartCustomerInteraction() called with: " + profile.customerName);

    currentCustomerProfile = profile;
    currentLine = 0;
    isDialogueActive = true;
    currentState = State.Intro;

    dialoguePanel.SetActive(true);
    
    if (responsePanel != null)
        responsePanel.SetActive(false);

    dialogueText.text = profile.introLines[Random.Range(0, profile.introLines.Length)];
}


   public void StartDialogue(string[] customLines)
{
    Debug.Log("⚠️ StartDialogue(string[]) called directly!");

    currentLine = 0;
    dialogueLines = customLines;
    dialoguePanel.SetActive(true);
    isDialogueActive = true;
    dialogueText.text = dialogueLines[currentLine];
}


void AdvanceDialogue()
{
    if (currentState == State.Intro)
    {
        dialogueText.text = currentCustomerProfile.orderLine;
        currentState = State.Order;
    }
    else if (currentState == State.Order)
    {
        ShowResponseOptions();
    }
}



void ShowResponseOptions()
{
    currentState = State.Choice;
    nextButton.gameObject.SetActive(false);
    responsePanel.SetActive(true);

    for (int i = 0; i < responseButtons.Length; i++)
    {
        int index = i;
        responseButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentCustomerProfile.playerResponseOptions[i];
        responseButtons[i].onClick.RemoveAllListeners();
        responseButtons[i].onClick.AddListener(() => SelectResponse(index));
    }
}

void SelectResponse(int index)
{
    string emotion = currentCustomerProfile.emotionalResults[index];
    int tip = currentCustomerProfile.tipModifiers[index];
    string reaction = currentCustomerProfile.reactionLines[index];

    Debug.Log($"🗯️ Customer reaction: {emotion} | Tip modifier: {tip}");

    dialogueText.text = reaction;
    responsePanel.SetActive(false);
    nextButton.gameObject.SetActive(false);

    // Show tip
    if (tipManager != null)
        tipManager.AddTip(tip);
    else
        Debug.LogWarning("⚠️ TipManager not assigned!");

    // Auto-exit after short delay
    StartCoroutine(ExitAfterDelay(2.5f));
}
IEnumerator ExitAfterDelay(float seconds)
{
    yield return new WaitForSeconds(seconds);
    EndDialogue();
    FindObjectOfType<CashierViewController>()?.ExitCashierMode();
}


void EndDialogue()
{
    isDialogueActive = false;
    dialoguePanel.SetActive(false);

    if (responsePanel != null)
        responsePanel.SetActive(false);

    if (nextButton != null)
        nextButton.gameObject.SetActive(false); 
}


}
