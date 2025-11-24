using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Treasure UI")]
    public GameObject treasureText;

    [Header("Battle UI")]
    public TextMeshProUGUI playerDiceText;
    public TextMeshProUGUI enemyDiceText;
    public TextMeshProUGUI enemyHPText;

    [Header("Player Stats")]
    public TextMeshProUGUI playerHPText; 
    public TextMeshProUGUI playerScoreText; 

    [Header("General Messages")]
    public TextMeshProUGUI messageText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void UpdateHP(int hp)
    {
        if (playerHPText != null)
            playerHPText.text = "HP: " + hp;
    }

    public void UpdateScore(int score)
    {
        if (playerScoreText != null)
            playerScoreText.text = "Score: " + score;
    }

    public void ShowTreasureUI(bool show)
    {
        if (treasureText != null)
            treasureText.SetActive(show);
    }

    public void ShowBattleResult(int playerDice, int enemyDice)
    {
        if (playerDiceText != null)
            playerDiceText.text = $"Player: {playerDice}";

        if (enemyDiceText != null)
            enemyDiceText.text = $"Enemy: {enemyDice}";
    }

    public void UpdateEnemyHP(int hp)
    {
        if (enemyHPText != null)
            enemyHPText.text = $"Enemy HP: {hp}";
    }

    public void ShowInteractMessage(string msg)
    {
        if (messageText != null)
        {
            messageText.text = msg;
            messageText.gameObject.SetActive(true);
        }
    }

    public void HideInteractMessage()
    {
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }
}
