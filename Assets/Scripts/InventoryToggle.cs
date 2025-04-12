using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryPanel;

    public void ToggleInventory()
    {
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }
}
