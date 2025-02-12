using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject playerPrefab; //Use to assign the player prefab
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");

        if (spawnPoint != null && playerPrefab != null)
        {
            Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("ERROR");
        }
    }
}
