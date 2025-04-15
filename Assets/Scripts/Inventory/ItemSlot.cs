using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents an inventory slot and manages item data.
/// </summary>
public class ItemSlot : MonoBehaviour
{
    public Image icon; // UI Image that displays the item icon
    private Item storedItem; // The item assigned to this slot

    /// <summary>
    /// Assigns an item to this slot.
    /// </summary>
  public void SetItem(Item item)
{
    storedItem = item;

    Debug.Log("ItemSlot.SetItem called with item: " + item?.itemName);

    if (icon == null)
    {
        Debug.LogError("ItemSlot icon reference is NULL!");
        return;
    }

    if (item == null)
    {
        Debug.LogError("Passed item is NULL!");
        return;
    }

    if (item.icon == null)
    {
        Debug.LogError("Item icon is NULL in item: " + item.itemName);
        return;
    }

    icon.sprite = item.icon;
    icon.enabled = true;
    Debug.Log("Successfully set icon for item: " + item.itemName);
}


    /// <summary>
    /// Clears the slot (for future item removal logic).
    /// </summary>
    public void ClearSlot()
    {
        storedItem = null;
        if (icon != null)
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }
}
