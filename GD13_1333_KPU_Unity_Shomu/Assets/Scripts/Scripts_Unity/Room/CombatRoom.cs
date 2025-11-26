using UnityEngine;

public class CombatRoom : Room
{
    [Header("Enemy Settings")]
    public int enemyHP = 1;
    private bool battleActive = false;

    private PlayerController player;

    private float inputCooldown = 0.2f;
    private float lastInputTime = 0f;

    protected override void OnPlayerEnter()
    {
        base.OnPlayerEnter();

        player = FindObjectOfType<PlayerController>();
        if (player != null) player.canMove = true;

        UIManager.Instance.ShowMessage("Press F to Battle");
        UIManager.Instance.UpdateEnemyHP(enemyHP);
    }

    protected override void OnPlayerExit()
    {
        base.OnPlayerExit();

        EndBattle();
        UIManager.Instance.ClearMessage();
    }

    public override void TriggerPlayerInteract()
    {
        if (Time.time - lastInputTime < inputCooldown)
            return;

        lastInputTime = Time.time;

        if (!battleActive)
            StartBattle();
        else
            ContinueBattle();
    }

    private void StartBattle()
    {
        battleActive = true;

        if (player != null) player.canMove = false;

        UIManager.Instance.ShowCombatUI();
        UIManager.Instance.ShowMessage("Press F to Roll Dice");
    }

    private void ContinueBattle()
    {
        if (!battleActive) return;

        int playerDice = Random.Range(1, 7);
        int enemyDice = Random.Range(1, 7);

        UIManager.Instance.UpdateDice(playerDice, enemyDice);

        if (playerDice > enemyDice)
        {
            enemyHP -= 1;
            UIManager.Instance.UpdateEnemyHP(enemyHP);
            UIManager.Instance.ShowMessage("Hit! Enemy -1 HP");

            if (enemyHP <= 0)
            {
                UIManager.Instance.ShowMessage("YOU WIN!");
                EndBattle();
            }
        }
        else if (playerDice < enemyDice)
        {
            GameState.TakeDamage(1);
            UIManager.Instance.ShowMessage("You took damage! -1 HP");
            EndBattle();
        }
        else
        {
            UIManager.Instance.ShowMessage("Draw");
            EndBattle();
        }
    }

    private void EndBattle()
    {
        battleActive = false;

        if (player != null) player.canMove = true;

        UIManager.Instance.HideCombatUI();
    }
}
