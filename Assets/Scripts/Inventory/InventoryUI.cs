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

    private void Start()
    {
        UpdateInventoryUI();
    }

    /// <summary>
    /// Updates the inventory UI to match the player's inventory.
    /// </summary>
 public void UpdateInventoryUI()
{
    Debug.Log("UpdateInventoryUI() called!"); // 🛠 Debug: Is this function even running?

    // Clear old slots
    foreach (Transform child in itemSlotContainer)
    {
        if (child != itemSlotTemplate.transform) // Keep template
        {
            Debug.Log("Destroying old slot..."); // 🛠 Debug: Is it destroying old slots?
            Destroy(child.gameObject);
        }
    }

    // Populate inventory slots
    foreach (Item item in InventoryManager.instance.GetInventory())
    {
        Debug.Log("Adding item to UI: " + item.itemName); // 🛠 Debug: Are items actually being added?

        GameObject newSlot = Instantiate(itemSlotTemplate, itemSlotContainer);
        newSlot.SetActive(true); // Enable the slot
        newSlot.GetComponent<Image>().sprite = item.icon; // Assign item icon
    }
}

}
