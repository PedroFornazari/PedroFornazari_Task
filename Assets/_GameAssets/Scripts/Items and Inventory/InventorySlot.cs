using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public CircleType itemType;
    public int amount;

    public enum CircleType
    {
        Point,
        Health,
        Speed,
        Special
    }
}
