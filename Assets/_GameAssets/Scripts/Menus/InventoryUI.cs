using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [System.Serializable]
    public class UISlot
    {
        public Image icon;
        public TextMeshProUGUI amountText;
    }

    [Header("Slots")]
    [SerializeField] private UISlot[] slots;

    public void Refresh()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].icon.enabled = false;
            slots[i].amountText.text = "";
        }

        for (int i = 0; i < InventoryManager.Instance.items.Count; i++)
        {
            InventorySlot item =
                InventoryManager.Instance.items[i];

            slots[i].icon.enabled = true;

            slots[i].amountText.text =
                item.amount.ToString();
        }
    }
}