using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager Instance;
    public Unit enemyUnit;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensures it stays between scenes
            Instance = this;
            
            /*********************
            these lines will reset the defeated enemy list when the game stops and restarts. 
            To prevent this, remove the following two lines:
            **********************/
            PlayerPrefs.DeleteKey("DefeatedEnemies");
            PlayerPrefs.Save();

            /*********************
            these lines will reset the collected item list when the game stops and restarts. 
            To prevent this, remove the following two lines:
            **********************/
            PlayerPrefs.DeleteKey("CollectedItems");
            PlayerPrefs.Save();
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates
        }
    }
}

