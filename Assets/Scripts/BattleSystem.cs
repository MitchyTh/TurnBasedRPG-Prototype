using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    public string currentScene;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    //Initial Action buttons
    public Button AttackButton;
    public Button SkillsButton;
    public Button ItemsButton;
    public Button ReturnButton;

    //Skill menu buttons
    //TODO

    public CanvasGroup ActionPanel;
    public CanvasGroup SkillPanel;

    Unit playerUnit;
    Unit enemyUnit;

    public TextMeshProUGUI dialogueText;
    public BattleState state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle(){
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        // Get enemy from EncounterManager if available
        if (EncounterManager.Instance.enemyUnit != null)
        {
            // enemyUnit = EncounterManager.Instance.enemyUnit;
            Debug.Log("Instantiating enemy: " + EncounterManager.Instance.enemyUnit.unitName);
            GameObject enemyGO = Instantiate(EncounterManager.Instance.enemyUnit.gameObject, enemyBattleStation);
            enemyUnit = enemyGO.GetComponent<Unit>();
        }
        else
        {
            // Fallback in case no enemy was set
            GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
            enemyUnit = enemyGO.GetComponent<Unit>();
        }

        //disable buttons
        AttackButton.interactable = false;
        SkillsButton.interactable = false;
        ItemsButton.interactable = false;

        dialogueText.text = enemyUnit.unitName + " approaches!";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn(){
        dialogueText.text = "Choose an action.";

        //enable buttons
        AttackButton.interactable = true;
        SkillsButton.interactable = true;
        ItemsButton.interactable = true;
    }

    IEnumerator PlayerAttack(){
        //disable buttons
        AttackButton.interactable = false;
        SkillsButton.interactable = false;
        ItemsButton.interactable = false;

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = playerUnit.unitName + " is attacking!";

        yield return new WaitForSeconds(2f);

        if(isDead){
            state = BattleState.WON;
            EndBattle();
        }else{
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    //!!!! TEMPORARY, ENEMY SIMPLY ATTACKS FOR NOW !!!!!!!!!!!!!!
    IEnumerator EnemyTurn(){
        //disable buttons
        AttackButton.interactable = false;
        SkillsButton.interactable = false;
        ItemsButton.interactable = false;

        dialogueText.text = enemyUnit.unitName + " attacks!";
        
        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);
        yield return new WaitForSeconds(1f);

        if(isDead){
            state = BattleState.LOST;
            EndBattle();
        }else{
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    public void LoadArea()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("PreviousScene", "DefaultScene"));
    }
    void EndBattle(){
        //disable buttons
        AttackButton.interactable = false;
        SkillsButton.interactable = false;
        ItemsButton.interactable = false;
        
        if (state == BattleState.WON){
            dialogueText.text = "You won the battle!";
        }
        else if (state == BattleState.LOST){
            dialogueText.text = "You were defeated.";
        }
        LoadArea();
    }


//Action menu buttons

    public void OnAttackButton(){
        if (state != BattleState.PLAYERTURN){
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnSkillsButton()
    {
        if (state != BattleState.PLAYERTURN) { 
            return;
        }

        ActionPanel.alpha = 0;
        ActionPanel.interactable = false;
        ActionPanel.blocksRaycasts = false;

        SkillPanel.alpha = 1;
        SkillPanel.interactable = true;
        SkillPanel.blocksRaycasts = true;

    }

    //Skill menu buttons
    public void OnReturnButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        ActionPanel.alpha = 1;
        ActionPanel.interactable = true;
        ActionPanel.blocksRaycasts = true;

        SkillPanel.alpha = 0;
        SkillPanel.interactable = false;
        SkillPanel.blocksRaycasts = false;

    }
}
