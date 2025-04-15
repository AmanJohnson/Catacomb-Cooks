using UnityEngine;

/// <summary>
/// Allows the player to pick up an item when they interact with it.
/// </summary>
public class ItemPickup : MonoBehaviour
{
    public Item item; // The item this object represents

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure only the player can pick it up
        {
            bool added = InventoryManager.instance.AddItem(item);
if (added)
    Destroy(gameObject);

        }
    }
}
