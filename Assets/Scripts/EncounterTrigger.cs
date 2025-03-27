using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterTrigger : MonoBehaviour
{
    public Unit enemyPrefab;
   // public TextMeshProUGUI dialogueText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //dialogueText.text = "You are going to do down!!";
        if (other.CompareTag("Player"))
        {

            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();
            EncounterManager.Instance.enemyUnit = enemyPrefab; // Save enemy data
            SceneManager.LoadScene("Battle-Screen");
        }
    }
}

