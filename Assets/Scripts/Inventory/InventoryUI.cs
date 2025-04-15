using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Handles updating the on-screen inventory UI when items are picked up.
/// </summary>
public class InventoryUI : MonoBehaviour
{
    public GameObject itemSlotTemplate; // UI template for an item slot
    public Transform itemSlotContainer; // Parent container for slots

    private int selectedIndex = -1; // Tracks the selected slot (-1 = none selected)
    private List<GameObject> slotObjects = new List<GameObject>(); // List of slot UI elements


    private void Start()
    {
        UpdateInventoryUI();
    }

    void Update()
{
    for (int i = 0; i < Mathf.Min(8, slotObjects.Count); i++)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1 + i) || Input.GetKeyDown(KeyCode.Keypad1 + i))
        {
            SelectSlot(i);
            break;
        }
    }
}


    /// <summary>
    /// Updates the inventory UI to match the player's inventory.
    /// </summary>
public void UpdateInventoryUI()
{
    if (itemSlotTemplate == null)
    {
        Debug.LogError("ERROR: `itemSlotTemplate` is NULL! Make sure the prefab is assigned.");
        return;
    }
    
    if (itemSlotContainer == null)
    {
        Debug.LogError("ERROR: `itemSlotContainer` is NULL! Assign it in the Inspector.");
        return;
    }

    if (InventoryManager.instance == null)
    {
        Debug.LogError("ERROR: `InventoryManager.instance` is NULL! Is the InventoryManager in the scene?");
        return;
    }

    if (InventoryManager.instance.GetInventory() == null)
    {
        Debug.LogError("ERROR: `InventoryManager.instance.GetInventory()` is NULL! Check if the inventory list is initialized.");
        return;
    }

    // Clear old slots
    foreach (Transform child in itemSlotContainer)
        Destroy(child.gameObject);

    slotObjects.Clear(); // Clear slot references

    // Populate slots
    for (int i = 0; i < InventoryManager.instance.GetInventory().Count; i++)
    {
        GameObject newSlot = Instantiate(itemSlotTemplate, itemSlotContainer);
newSlot.SetActive(true);

var itemSlotComponent = newSlot.GetComponent<ItemSlot>();

if (itemSlotComponent == null)
{
    Debug.LogError("❌ ItemSlot component missing from newSlot!");
}
else
{
    Debug.Log("✅ ItemSlot component found, calling SetItem()...");
    itemSlotComponent.SetItem(InventoryManager.instance.GetInventory()[i]);
}


        int slotIndex = i; // Capture index for lambda function
        newSlot.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => SelectSlot(slotIndex));

        slotObjects.Add(newSlot); // Store reference for highlighting
    }

    UpdateSlotHighlight(); // Ensure selection highlight is updated
}


public void SelectSlot(int index)
{
    if (selectedIndex == index) return; // Avoid re-selecting the same slot

    selectedIndex = index;
    Debug.Log("Selected slot: " + index);
    
    UpdateSlotHighlight(); // Update the visual selection
}

private void UpdateSlotHighlight()
{
    for (int i = 0; i < slotObjects.Count; i++)
    {
        UnityEngine.UI.Image slotImage = slotObjects[i].GetComponent<UnityEngine.UI.Image>();
        if (i == selectedIndex)
            slotImage.color = Color.yellow; // Highlight selected slot
        else
            slotImage.color = Color.white; // Default color
    }
}


}
