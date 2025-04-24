using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterTrigger : MonoBehaviour
{
    public Unit enemyPrefab;
    public GameObject enemyGameObject;

    // public TextMeshProUGUI dialogueText;

    /****************************************************************************
    All defeated enemies in the game are stored in PlayerPrefs "DefeatedEnemies".
    This will be used to remove them from the overworld once defeated.
    Since our game is small, this will work for now.
    ****************************************************************************/
    void Start()
    {
        RemoveDefeatedEnemiesOnLoad(); //remove enemies from overworld that were previously defeated if re-entering

        //check if coming from battle scene and remove enemy as needed
        int battleWon = PlayerPrefs.GetInt("BattleWon", 0);

        if (battleWon == 1)
        {
            RemoveDefeatedEnemy(); //remove overworld enemy only if battle was won
            PlayerPrefs.SetInt("BattleWon", 0); // reset battle state
            PlayerPrefs.Save();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //dialogueText.text = "You are going to go down!!";
        if (other.CompareTag("Player"))
        {

            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();

            PlayerPrefs.SetString("CurrentEnemyObject", enemyGameObject.name); //to be used to delete enemy after battle won
            PlayerPrefs.Save();
            
            EncounterManager.Instance.enemyUnit = enemyPrefab; // Save enemy data
            SceneManager.LoadScene("Battle-Screen");

        }
    }

    /**********************************************
    these methods are used to delete enemies in the overworld after battle is won, permanently. 
    **********************************************/

    public void MarkEnemyDefeated(string enemyName){
        string defeatedEnemies = PlayerPrefs.GetString("DefeatedEnemies", "");
        
        if (!defeatedEnemies.Contains(enemyName))
        {
            defeatedEnemies += enemyName + ";"; // store multiple enemies with a separator
            PlayerPrefs.SetString("DefeatedEnemies", defeatedEnemies);
            PlayerPrefs.Save();
        }
    }

    public void RemoveDefeatedEnemy()
    {
        string enemyName = PlayerPrefs.GetString("CurrentEnemyObject", "");
        if (!string.IsNullOrEmpty(enemyName))
        {
            MarkEnemyDefeated(enemyName);

            GameObject enemy = GameObject.Find(enemyName);

            if (enemy != null)
            {
                Destroy(enemy); // remove the enemy from overworld
            }
            PlayerPrefs.DeleteKey("CurrentEnemyObject"); // clear saved enemy
        }
    }

    private void RemoveDefeatedEnemiesOnLoad()
    {
        string defeatedEnemies = PlayerPrefs.GetString("DefeatedEnemies", "");
        if (!string.IsNullOrEmpty(defeatedEnemies))
        {
            string[] enemyNames = defeatedEnemies.Split(';');
            foreach (string enemyName in enemyNames)
            {
                if (!string.IsNullOrEmpty(enemyName))
                {
                    GameObject enemy = GameObject.Find(enemyName);
                    if (enemy != null)
                    {
                        Destroy(enemy); // remove the defeated enemy on scene load
                    }
                }
            }
        }
    }
}

