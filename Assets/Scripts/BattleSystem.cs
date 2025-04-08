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
    /**********
        BATTLE WON TRACKED IN PlayerPrefs.GetInt("BattleWon", "")
     **********/
    public string currentScene;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    //Initial Action buttons
    public Button AttackButton;
    public Button SkillChoiceButton;
    public Button ItemsButton;
    public Button ReturnButton;

    //Skill menu buttons
    public GameObject SkillButtonPrefab;
    public Transform SkillButtonContainer;

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

    IEnumerator SetupBattle() {
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
        SkillChoiceButton.interactable = false;
        ItemsButton.interactable = false;

        dialogueText.text = enemyUnit.unitName + " approaches!";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn() {
        dialogueText.text = "Choose an action.";

        //enable buttons
        AttackButton.interactable = true;
        SkillChoiceButton.interactable = true;
        ItemsButton.interactable = true;

        SpawnSkillMenuButtons();

    }

    IEnumerator PlayerAttack() {
        //disable buttons
        AttackButton.interactable = false;
        SkillChoiceButton.interactable = false;
        ItemsButton.interactable = false;

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = playerUnit.unitName + " is attacking!";

        yield return new WaitForSeconds(2f);

        if (isDead) {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        } else {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    //!!!! TEMPORARY, ENEMY SIMPLY ATTACKS FOR NOW !!!!!!!!!!!!!!
    IEnumerator EnemyTurn() {
        //disable buttons
        AttackButton.interactable = false;
        SkillChoiceButton.interactable = false;
        ItemsButton.interactable = false;

        dialogueText.text = enemyUnit.unitName + " attacks!";


        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        
        yield return new WaitForSeconds(1f);

        playerHUD.SetHP(playerUnit.currentHP);

        if (isDead) {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        } else {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void LoadArea()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("PreviousScene", "DefaultScene"));
        PlayerPrefs.DeleteKey("PreviousScene"); //clear stored value

    }
    IEnumerator EndBattle() {
        //disable buttons
        AttackButton.interactable = false;
        SkillChoiceButton.interactable = false;
        ItemsButton.interactable = false;

        if (state == BattleState.WON) {
            dialogueText.text = "You won the battle!";
            PlayerPrefs.SetInt("BattleWon", 1); //track battle state
            PlayerPrefs.Save();
        }
        else if (state == BattleState.LOST) {
            dialogueText.text = "You were defeated.";
            PlayerPrefs.SetInt("BattleWon", 0);
            PlayerPrefs.Save();
        }

        yield return new WaitForSeconds(2f); //wait before returning to overworld
        
        LoadArea();
    }


    //Action menu buttons

    public void OnAttackButton() {
        if (state != BattleState.PLAYERTURN) {
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

    public IEnumerator UseSkill(SkillBase skill)
    {
        skill.UseSkill(playerUnit, enemyUnit);

        AttackButton.interactable = false;
        SkillChoiceButton.interactable = false;
        ItemsButton.interactable = false;

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = playerUnit.unitName + " is using " + skill.name + "!";

        yield return new WaitForSeconds(2f);

        OnReturnButton();

        if (isDead)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    public void SpawnSkillMenuButtons()
    {
        //Clear skill buttons
        foreach (Transform child in SkillButtonContainer)
        {
            Destroy(child.gameObject);
        }

        //Skill button spawning
        foreach (SkillBase skill in playerUnit.skills)
        {
            SkillBase capturedSkill = skill;  // avoid closure issue
            GameObject buttonGO = Instantiate(SkillButtonPrefab, SkillButtonContainer);
            Button button = buttonGO.GetComponent<Button>();
            TextMeshProUGUI buttonText = buttonGO.GetComponentInChildren<TextMeshProUGUI>();

            buttonText.text = capturedSkill.name;

            button.onClick.AddListener(() => StartCoroutine(UseSkill(capturedSkill)));
        }
    }
}
