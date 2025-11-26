using UnityEngine;
using UnityEngine.UI;
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
    public GameObject GameClear;   
    public GameObject GameOver;    

    [HideInInspector]
    public bool isGameFrozen = false; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        combatPanel.SetActive(false);
        messageText.text = "";

        if (GameClear != null) GameClear.SetActive(false);
        if (GameOver != null) GameOver.SetActive(false);

        GameState.OnGameClear += ShowGameClear;
        GameState.OnGameOver += ShowGameOver;
    }

    public void UpdateHP(int hp) => hpText.text = "HP: " + hp;
    public void UpdateScore(int score) => scoreText.text = "Score: " + score;

    public void ShowMessage(string msg) => messageText.text = msg;
    public void ClearMessage() => messageText.text = "";

    public void ShowCombatUI()
    {
        combatPanel.SetActive(true);
        resultText.text = "";
    }

    public void HideCombatUI() => combatPanel.SetActive(false);

    public void UpdateDice(int playerDice, int enemyDice)
    {
        playerDiceText.text = "Player Dice: " + playerDice;
        enemyDiceText.text = "Enemy Dice: " + enemyDice;
    }

    public void UpdateEnemyHP(int hp) => enemyHPText.text = "Enemy HP: " + hp;

    public void ShowTreasureUI(bool show)
    {
        if (treasureText != null)
            treasureText.SetActive(show);
    }

    public void ShowGameClear()
    {
        isGameFrozen = true;

        if (GameClear != null)
            GameClear.SetActive(true);
    }

    public void ShowGameOver()
    {
        isGameFrozen = true;

        if (GameOver != null)
            GameOver.SetActive(true);
    }
}
