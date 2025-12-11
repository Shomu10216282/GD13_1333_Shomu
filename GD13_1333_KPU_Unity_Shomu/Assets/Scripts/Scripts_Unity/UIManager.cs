using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Player UI")]
    public TMP_Text hpText;
    public TMP_Text scoreText;
    public TMP_Text messageText;

    [Header("Combat UI")]
    public GameObject combatPanel;
    public TMP_Text playerDiceText;
    public TMP_Text enemyDiceText;
    public TMP_Text enemyHPText;
    public TMP_Text resultText;

    [Header("Treasure UI")]
    public GameObject treasureText;

    [Header("Game End UI")]
    public GameObject gameClearPanel;
    public GameObject gameOverPanel;

    [Header("Pause UI")]
    public GameObject pausePanel;

    [HideInInspector]
    public bool isGameFrozen = false;

    private int maxScore = 10;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        combatPanel.SetActive(false);
        pausePanel?.SetActive(false);
        treasureText?.SetActive(false);
        gameClearPanel?.SetActive(false);
        gameOverPanel?.SetActive(false);

        GameState.OnGameClear += ShowGameClear;
        GameState.OnGameOver += ShowGameOver;
        GameState.Initialize();
    }

    public void SetMaxScore(int value)
    {
        maxScore = value;
        UpdateScore(GameState.Score);
    }

    public void UpdateHP(int hp) => hpText.text = "HP: " + hp;

    public void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}/{10}";
    }

    public void ShowMessage(string msg) => messageText.text = msg;
    public void ClearMessage() => messageText.text = "";

    public void ShowCombatUI()
    {
        combatPanel.SetActive(true);
        resultText.text = "";
    }

    public void HideCombatUI()
    {
        combatPanel.SetActive(false);
        resultText.text = "";
    }

    public void UpdateDice(int playerDice, int enemyDice)
    {
        playerDiceText.text = "Player Dice: " + playerDice;
        enemyDiceText.text = "Enemy Dice: " + enemyDice;
    }

    public void UpdateEnemyHP(int hp) => enemyHPText.text = "Enemy HP: " + hp;

    public void ShowBattleResult(string message)
    {
        resultText.text = message;
    }

    public void HideBattleResult()
    {
        resultText.text = "";
    }

    public void ShowTreasureUI(bool show)
    {
        if (treasureText != null)
            treasureText.SetActive(show);
    }

    public void ShowGameClear()
    {
        isGameFrozen = true;
        gameClearPanel?.SetActive(true);
    }

    public void ShowGameOver()
    {
        isGameFrozen = true;
        gameOverPanel?.SetActive(true);
    }

    public void TogglePauseUI()
    {
        if (combatPanel.activeSelf || gameClearPanel.activeSelf || gameOverPanel.activeSelf) return;

        bool isActive = pausePanel.activeSelf;
        pausePanel.SetActive(!isActive);

        isGameFrozen = !isActive;
    }
}
