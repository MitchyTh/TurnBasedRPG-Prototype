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
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates
        }
    }
}

