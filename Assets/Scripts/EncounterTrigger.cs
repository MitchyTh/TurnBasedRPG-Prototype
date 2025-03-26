using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterTrigger : MonoBehaviour
{
    public Unit enemyPrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();
            EncounterManager.Instance.enemyUnit = enemyPrefab; // Save enemy data
            SceneManager.LoadScene("Battle-Screen");
        }
    }
}

