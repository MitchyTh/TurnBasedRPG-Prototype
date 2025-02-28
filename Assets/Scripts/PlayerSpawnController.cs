using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnManager : MonoBehaviour
{
    public string spawnObjectName;  // Name of the spawn object in the scene
    private GameObject spawnObject;

    private void Start()
    {
        // Find the spawn object in the scene by its name
        spawnObject = GameObject.Find(spawnObjectName);

        // Ensure the spawn object exists
        if (spawnObject != null)
        {
            // Set the player's position to the spawn object's position
            MovePlayerToSpawn();
        }
        else
        {
            Debug.LogError("Spawn object not found in the scene.");
        }
    }

    private void MovePlayerToSpawn()
    {
        // Assuming the player is a GameObject in the scene and you have a reference to it
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Move the player to the spawn position
            player.transform.position = spawnObject.transform.position;
        }
        else
        {
            Debug.LogError("Player not found in the scene.");
        }
    }
}
