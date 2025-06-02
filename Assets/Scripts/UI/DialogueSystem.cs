using UnityEngine;
using UnityEngine.UI;
using TMPro;

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


    void Start()
{
    // Force it to always reset to known state
    dialoguePanel.SetActive(false);
    isDialogueActive = false;

    // Log the reference to confirm
    Debug.Log("▶️ dialoguePanel assigned to: " + dialoguePanel.name);

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
        if (isDialogueActive && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)))
        {
            AdvanceDialogue();
        }
    }

    public void StartCustomerInteraction(CustomerProfile profile)
{
    currentCustomerProfile = profile;
    currentLine = 0;
    isDialogueActive = true;
    dialoguePanel.SetActive(true);

    // Show intro first
    dialogueText.text = profile.introLines[Random.Range(0, profile.introLines.Length)];

    // Later we'll move to order, then choices
}


   public void StartDialogue(string[] customLines)
{
    currentLine = 0;
    dialogueLines = customLines;
    dialoguePanel.SetActive(true);
    isDialogueActive = true;
    dialogueText.text = dialogueLines[currentLine];
}


    void AdvanceDialogue()
    {
        currentLine++;
        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
    }
}
