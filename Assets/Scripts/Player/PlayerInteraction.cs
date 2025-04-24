using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionController : MonoBehaviour
{
    // Twelve-slot inventory to hold picked up items.
    public ItemData[] inventory = new ItemData[12];

    public InventoryUIManager inventoryUI; //used to update inventory UI

    // Reference to the interaction textbox UI (e.g., a panel with a Text component).
    // Ensure this GameObject is initially inactive in the scene.
    public GameObject interactionTextBox;
    // Reference to the Text component within the textbox.
    public TextMeshProUGUI interactionText;

    // Distance for the raycast used for interactions.
    public float interactionDistance = 2f;

    // The direction the player is currently facing.
    // This is updated based on movement input.
    private Vector2 lastDirection = Vector2.right; // default facing right

    void Update()
    {
        // Update the facing direction based on input.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 inputDir = new Vector2(h, v);
        if (inputDir != Vector2.zero)
        {
            lastDirection = inputDir.normalized;
        }

        //slightly offset raycast origin to avoid glitching with player box collider
        Vector2 origin = (Vector2)transform.position + lastDirection * 0.5f; // offset by 0.5 units
        RaycastHit2D hit = Physics2D.Raycast(origin, lastDirection, interactionDistance);
        Debug.DrawRay(origin, lastDirection * interactionDistance, Color.red, 1f);


        // When the player presses E, attempt to pick up an item.
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (hit.collider != null && hit.collider.CompareTag("Item"))
            {
                GameObject item = hit.collider.gameObject;
                WorldItem worldItem = item.GetComponent<WorldItem>();

                if (worldItem != null && worldItem.itemData != null)
                {
                    // Add to inventory
                    pickUp(worldItem.itemData);
                    Debug.Log("✅ Picked up: " + worldItem.itemData.itemName);

                    // Mark the item as collected for persistence
                    CollectedItemManager manager = FindFirstObjectByType<CollectedItemManager>();
                    if (manager != null)
                    {
                        manager.MarkItemCollected(item.name);
                        Debug.Log("✅ Marked collected: " + item.name);
                    }
                    else
                    {
                        Debug.LogWarning("❌ CollectedItemManager not found");
                    }

                    // Remove the item from the scene
                    Destroy(item);
                }
                else
                {
                    Debug.LogWarning("❌ Item missing WorldItem or ItemData");
                }
            }
        }

        // When the player presses F, look at the object (for "Inspectable" objects).
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (hit.collider != null && hit.collider.CompareTag("Inspectable"))
            {
                lookAt(hit.collider.gameObject);
            }
        }

        // When the player presses T, talk to NPCs or Enemies.
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (hit.collider != null && (hit.collider.CompareTag("NPC") || hit.collider.CompareTag("Enemy")))
            {
                talkTo(hit.collider.gameObject);
            }
        }
    }

    // pickUp: Adds the specified item to the inventory if there is an empty slot.
    public void pickUp(ItemData item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                Debug.Log("Picked up: " + item.name);

                inventoryUI.UpdateUI(inventory);
                return;
            }
        }
        Debug.Log("Inventory full! Cannot pick up " + item.name);
    }

    //if X button is clicked in item inventory, drop the item
    public void dropItem(int index)
{
    if (index >= 0 && index < inventory.Length && inventory[index] != null)
    {
        ItemData item = inventory[index];

        GameObject dropped = Instantiate(item.prefab, transform.position + Vector3.down * 0.5f, Quaternion.identity);
        Destroy(dropped, 0.5f); // disappears after 1 second

        Debug.Log("🗑 Dropped and destroyed: " + item.itemName);

        inventory[index] = null;

        inventoryUI.UpdateUI(inventory);
    }
}


    // lookAt: Activates the textbox and displays a description for the inspected item.
    void lookAt(GameObject item)
    {
        interactionTextBox.SetActive(true);

        WorldItem worldItem = item.GetComponent<WorldItem>();
        if (worldItem != null && worldItem.itemData != null)
        {
            interactionText.text = worldItem.itemData.description;
            Debug.Log("Looking at: " + worldItem.itemData.itemName);
        }
        else
        {
            interactionText.text = "It's a " + item.name;
            Debug.Log("Looking at: " + item.name);
        }
    }

    // talkTo: Activates the textbox and displays dialogue when talking to an NPC or enemy.
    public void talkTo(GameObject npc)
    {
        interactionTextBox.SetActive(true);
        // For demonstration, display a placeholder dialogue message.
        // You can extend this functionality with a dialogue system.
        interactionText.text = "Hello there " + npc.name;
        Debug.Log("Talking to: " + npc.name);
    }
}

