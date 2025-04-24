// This script is used to maintain all of the collectible items in the world to prevent duplicates
// spawning on scene reloads. System is similar to enemy persistence

using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectedItemManager : MonoBehaviour
{
    private static CollectedItemManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RemoveCollectedItemsOnLoad(); // do this again on every scene load
    }

    public void MarkItemCollected(string itemName)
    {
        string collectedItems = PlayerPrefs.GetString("CollectedItems", "");

        if (!collectedItems.Contains(itemName))
        {
            collectedItems += itemName + ";";
            PlayerPrefs.SetString("CollectedItems", collectedItems);
            PlayerPrefs.Save();

            Debug.Log(PlayerPrefs.GetString("CollectedItems", ""));
        }
    }

    private void RemoveCollectedItemsOnLoad()
    {
        string collectedItems = PlayerPrefs.GetString("CollectedItems", "");
        Debug.Log("🧾 Loaded collected items: " + collectedItems);

        if (!string.IsNullOrEmpty(collectedItems))
        {
            string[] itemNames = collectedItems.Split(';');
            foreach (string itemName in itemNames)
            {
                if (!string.IsNullOrEmpty(itemName))
                {
                    GameObject item = GameObject.Find(itemName);
                    if (item != null)
                    {
                        Debug.Log("❌ Destroying collected item: " + item.name);
                        Destroy(item);
                    }
                    else
                    {
                        Debug.LogWarning("⚠️ Could not find item: " + itemName);
                    }
                }
            }
        }
    }
}
