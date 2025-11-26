using UnityEngine;

public class CombatRoom : Room
{
    [Header("Enemy Settings")]
    public int enemyHP = 1;
    private bool battleActive = false;
    private bool battleDone = false;

    private PlayerController player;

    private float inputCooldown = 0.2f;
    private float lastInputTime = 0f;

    protected override void OnPlayerEnter()
    {
        base.OnPlayerEnter();

        player = FindObjectOfType<PlayerController>();
        if (player != null) player.canMove = true;

        if (!battleDone)
        {
            UIManager.Instance.ShowMessage("Press F to Battle");
        }
        else
        {
            UIManager.Instance.ShowMessage("This room's battle is already cleared.");
        }

        UIManager.Instance.UpdateEnemyHP(enemyHP);
    }

    protected override void OnPlayerExit()
    {
        base.OnPlayerExit();

        UIManager.Instance.ClearMessage();
        UIManager.Instance.HideCombatUI();
        UIManager.Instance.HideBattleResult();
    }

    public override void TriggerPlayerInteract()
    {
        if (Time.time - lastInputTime < inputCooldown)
            return;

        lastInputTime = Time.time;

        if (battleDone)
        {
            UIManager.Instance.ShowMessage("This room's battle is already cleared.");
            return;
        }

        if (!battleActive)
            StartBattle();
        else
            ContinueBattle();
    }

    private void StartBattle()
    {
        battleActive = true;

        if (player != null) player.canMove = false;

        UIManager.Instance.ClearMessage();

        UIManager.Instance.ShowCombatUI();

        UIManager.Instance.UpdateDice(0, 0);

        UIManager.Instance.ShowBattleResult("Press F to Roll Dice");
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
            UIManager.Instance.ShowBattleResult("Hit! Enemy -1 HP");

            if (enemyHP <= 0)
            {
                battleDone = true;
                UIManager.Instance.ShowBattleResult("YOU WIN! (Press F to close)");
                EndBattle();
            }
        }
        else if (playerDice < enemyDice)
        {
            GameState.TakeDamage(1);
            UIManager.Instance.ShowBattleResult("You took damage! -1 HP (Press F to close)");

            battleDone = true;
            EndBattle();
        }
        else
        {
            UIManager.Instance.ShowBattleResult("Draw (Press F to close)");

            battleDone = true;
            EndBattle();
        }
    }

    private void EndBattle()
    {
        battleActive = false;

        if (player != null) player.canMove = true;

        StartCoroutine(WaitForClose());
    }

    private System.Collections.IEnumerator WaitForClose()
    {
        yield return new WaitForSeconds(0.1f);

        bool closed = false;

        while (!closed)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                UIManager.Instance.HideCombatUI();
                UIManager.Instance.HideBattleResult();

                UIManager.Instance.ShowMessage("This room's battle is already cleared.");

                closed = true;
            }
            yield return null;
        }
    }
}
