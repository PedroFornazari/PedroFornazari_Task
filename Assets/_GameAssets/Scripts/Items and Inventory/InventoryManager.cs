using System.Collections.Generic;
using UnityEngine;
using static InventorySlot;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Inventory")]
    [SerializeField] private int maxSlots = 4;

    public List<InventorySlot> items = new();
    public int inventoryScore = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        inventoryScore = 0;
    }

    public bool AddItem(CircleType type)
    {
        Debug.Log("Coletou");
        inventoryScore += 1;
        InventorySlot existingItem = items.Find(i => i.itemType == type);

        if (existingItem != null)
        {
            existingItem.amount++;
            return true;
        }

        if (items.Count >= maxSlots)
            return false;

        InventorySlot newItem = new InventorySlot
        {
            itemType = type,
            amount = 1
        };

        items.Add(newItem);
        FindFirstObjectByType<InventoryUI>()?.Refresh();

        return true;
    }

    public bool RemoveItem(CircleType type)
    {
        InventorySlot item = items.Find(i => i.itemType == type);

        if (item == null)
            return false;

        item.amount--;

        if (item.amount <= 0)
            items.Remove(item);

        FindFirstObjectByType<InventoryUI>()?.Refresh();
        return true;
    }

    public int GetAmount(CircleType type)
    {
        InventorySlot item = items.Find(i => i.itemType == type);

        if (item == null)
            return 0;

        return item.amount;
    }
}