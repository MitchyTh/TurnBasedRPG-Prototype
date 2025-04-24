using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryUIManager : MonoBehaviour
{
    private static bool isInstantiated = false;

    [System.Serializable]
    public class InventorySlotUI
    {
        public UnityEngine.UI.Image itemImage;
        public Button deleteButton;
    }

    public InventorySlotUI[] slots;


    void Awake()
    {
        if (!isInstantiated)
        {
            DontDestroyOnLoad(gameObject);
            isInstantiated = true;
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // disable inventory in the battle scene
        if (scene.name == "Battle-Screen")
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

        // Rebind buttons to the current scene's UI elements
        PlayerInteractionController player = FindFirstObjectByType<PlayerInteractionController>();
        for (int i = 0; i < slots.Length; i++)
        {
            int index = i;

            if (slots[i].deleteButton != null)
            {
                slots[i].deleteButton.onClick.RemoveAllListeners();
                slots[i].deleteButton.onClick.AddListener(() => player.dropItem(index));
            }
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    // Call this method when the inventory changes
    public void UpdateUI(ItemData[] inventory)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.Length && inventory[i] != null)
            {
                slots[i].itemImage.sprite = inventory[i].icon;
                slots[i].itemImage.color = Color.white;
                slots[i].deleteButton.interactable = true;
            }
            else
            {
                slots[i].itemImage.sprite = null;
                slots[i].itemImage.color = new Color(1, 1, 1, 0); // transparent
                slots[i].deleteButton.interactable = false;
            }
        }
    }
}
