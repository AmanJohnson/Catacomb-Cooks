using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the player's inventory, storing collected items.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance; // Singleton for easy access
    public InventoryUI inventoryUI; // Reference to UI script
    private List<Item> inventory = new List<Item>(); // Stores collected items

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// Adds an item to the inventory.
    /// </summary>
   public void AddItem(Item newItem)
{
    inventory.Add(newItem);
    Debug.Log("Picked up: " + newItem.itemName);

    if (inventoryUI != null)
    {
        Debug.Log("Updating UI..."); // 🛠 Debug: Is UI being updated?
        inventoryUI.UpdateInventoryUI();
    }
    else
    {
        Debug.LogError("inventoryUI is NULL! Make sure it's assigned in the Inspector.");
    }

    PrintInventory();
}


        /// <summary>
    /// Returns the list of items in the inventory.
    /// </summary>
    public List<Item> GetInventory()
    {
        return inventory;
    }


    /// <summary>
    /// Prints the inventory contents to the console.
    /// </summary>
    private void PrintInventory()
    {
        Debug.Log("Current Inventory:");
        foreach (Item item in inventory)
        {
            Debug.Log("- " + item.itemName);
        }
    }
}
