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
    public int maxInventorySize = 8; // Set the limit (adjust as needed)

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// Adds an item to the inventory.
    /// </summary>
public bool AddItem(Item newItem)
{
    if (inventory.Count >= 8)
    {
        Debug.Log("Inventory full. Cannot add: " + newItem.itemName);
        return false;
    }

    inventory.Add(newItem);
    Debug.Log("Picked up: " + newItem.itemName);

    if (inventoryUI != null)
        inventoryUI.UpdateInventoryUI();

    PrintInventory();
    return true;
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
