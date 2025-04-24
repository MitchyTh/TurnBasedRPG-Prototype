using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryUIManager : MonoBehaviour
{
    private static bool isInstantiated = false;
    public UnityEngine.UI.Image[] slotImages;

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


    // Call this method when the inventory changes
    public void UpdateUI(GameObject[] inventory)
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (i < inventory.Length && inventory[i] != null)
            {
                SpriteRenderer sr = inventory[i].GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    slotImages[i].sprite = sr.sprite;
                    slotImages[i].color = Color.white; // make sure it's visible
                }
            }
            else
            {
                slotImages[i].sprite = null;
                slotImages[i].color = new Color(1,1,1,0); // hide if empty
            }
        }
    }
}
