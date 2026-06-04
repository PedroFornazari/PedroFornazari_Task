using UnityEngine;
using static InventorySlot;

public class CircleItem : MonoBehaviour
{
    [Header("Item Data")]
    public CircleType circleType;

    [Header("Special Circle")]
    public string specialID;
    public string specialName;
    [TextArea]
    public string description;
}