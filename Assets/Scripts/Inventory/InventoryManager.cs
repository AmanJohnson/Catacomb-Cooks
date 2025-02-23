using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the player's inventory, storing collected items.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance; // Singleton for easy access
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
        PrintInventory();
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
