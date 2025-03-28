using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionController : MonoBehaviour
{
    // Three-slot inventory to hold picked up items.
    public GameObject[] inventory = new GameObject[3];

    // Reference to the interaction textbox UI (e.g., a panel with a Text component).
    // Ensure this GameObject is initially inactive in the scene.
    public GameObject interactionTextBox;
    // Reference to the Text component within the textbox.
    public Text interactionText;

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

        // When the player presses E, attempt to pick up an item.
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, lastDirection, interactionDistance);
            if (hit.collider != null && hit.collider.CompareTag("Item"))
            {
                pickUp(hit.collider.gameObject);
            }
        }

        // When the player presses F, look at the object (for "Inspectable" objects).
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, lastDirection, interactionDistance);
            if (hit.collider != null && hit.collider.CompareTag("Inspectable"))
            {
                lookAt(hit.collider.gameObject);
            }
        }

        // When the player presses T, talk to NPCs or Enemies.
        if (Input.GetKeyDown(KeyCode.T))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, lastDirection, interactionDistance);
            if (hit.collider != null && (hit.collider.CompareTag("NPC") || hit.collider.CompareTag("Enemy")))
            {
                talkTo(hit.collider.gameObject);
            }
        }
    }

    // pickUp: Adds the specified item to the inventory if there is an empty slot.
    void pickUp(GameObject item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                // Optionally remove the item from the scene.
                item.SetActive(false);
                Debug.Log("Picked up: " + item.name);
                return;
            }
        }
        Debug.Log("Inventory full! Cannot pick up " + item.name);
    }

    // lookAt: Activates the textbox and displays a description for the inspected item.
    void lookAt(GameObject item)
    {
        interactionTextBox.SetActive(true);
        // For demonstration, simply display the object's name.
        // You can extend this to show a detailed description by using an additional component on the item.
        interactionText.text = "It's a " + item.name;
        Debug.Log("Looking at: " + item.name);
    }

    // talkTo: Activates the textbox and displays dialogue when talking to an NPC or enemy.
    void talkTo(GameObject npc)
    {
        interactionTextBox.SetActive(true);
        // For demonstration, display a placeholder dialogue message.
        // You can extend this functionality with a dialogue system.
        interactionText.text = "Hello there " + npc.name;
        Debug.Log("Talking to: " + npc.name);
    }
}

