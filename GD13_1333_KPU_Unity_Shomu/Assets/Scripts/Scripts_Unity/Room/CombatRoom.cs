using UnityEngine;

public class CombatRoom : Room
{
    [Header("Enemy Settings")]
    public int enemyHP = 1;
    private bool playerInRoom = false;
    private bool battleActive = false;

    private void Update()
    {
        if (playerInRoom && Input.GetKeyDown(KeyCode.F))
        {
            if (!battleActive)
            {
                StartBattle();
            }
            else
            {
                ContinueBattle();
            }
        }
    }

    protected override void OnPlayerEnter()
    {
        playerInRoom = true;
        UIManager.Instance.ShowMessage("Press F to Battle");
        UIManager.Instance.UpdateEnemyHP(enemyHP);
    }

    protected override void OnPlayerExit()
    {
        playerInRoom = false;
        UIManager.Instance.ClearMessage();
        UIManager.Instance.HideCombatUI();
    }

    void StartBattle()
    {
        battleActive = true;
        UIManager.Instance.ShowCombatUI();
        UIManager.Instance.ShowMessage("Press F to Roll Dice");
    }

    void ContinueBattle()
    {
        int playerDice = Random.Range(1, 7);
        int enemyDice = Random.Range(1, 7);

        UIManager.Instance.UpdateDice(playerDice, enemyDice);

        if (playerDice > enemyDice)
        {
            enemyHP -= 1;
            UIManager.Instance.UpdateEnemyHP(enemyHP);

            if (enemyHP <= 0)
            {
                UIManager.Instance.ShowBattleResult("YOU WIN!");
                UIManager.Instance.ShowMessage("Enemy Defeated!");

                GameState.AddScore(1);

                battleActive = false;
                return;
            }

            UIManager.Instance.ShowBattleResult("Hit! Enemy -1 HP");
        }
        else if (playerDice < enemyDice)
        {
            GameState.TakeDamage(1);
            UIManager.Instance.ShowBattleResult("You took damage! -1 HP");
        }
        else
        {
            UIManager.Instance.ShowBattleResult("Draw");
        }

        UIManager.Instance.ShowMessage("Press F to Roll Again");
    }
}
