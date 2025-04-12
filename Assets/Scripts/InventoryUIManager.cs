using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryUIManager : MonoBehaviour
{
    private static bool isInstantiated = false;

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
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
