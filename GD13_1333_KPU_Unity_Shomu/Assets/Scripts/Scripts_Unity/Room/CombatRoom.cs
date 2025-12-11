using UnityEngine;
using System.Collections;

public class CombatRoom : Room
{
    [Header("Enemy Settings")]
    public int enemyHP = 1;
    private bool battleActive = false;
    private bool battleDone = false;

    private PlayerController player;

    private float inputCooldown = 0.2f;
    private float lastInputTime = 0f;

    [Header("Audio Settings")]
    public AudioClip diceClip;
    private AudioSource diceAudio;

    public float diceResultDelay = 0.1f;

    private bool waitingForClose = false;

    private void Start()
    {
        diceAudio = gameObject.AddComponent<AudioSource>();
        diceAudio.playOnAwake = false;
        diceAudio.loop = false;
        diceAudio.volume = 1f;
    }

    protected override void OnPlayerEnter()
    {
        base.OnPlayerEnter();

        player = FindObjectOfType<PlayerController>();
        if (player != null) player.canMove = true;

        if (!battleDone)
            UIManager.Instance.ShowMessage("Press F to Battle");
        else
            UIManager.Instance.ShowMessage("This room's battle is already cleared.");

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

        if (waitingForClose)
        {
            CloseBattleMessage();
            return;
        }

        if (battleDone)
        {
            UIManager.Instance.ShowMessage("This room's battle is already cleared.");
            return;
        }

        if (!battleActive)
            StartBattle();
        else
            StartCoroutine(DiceRoutine());
    }

    private void StartBattle()
    {
        battleActive = true;

        if (player != null) player.canMove = false;

        UIManager.Instance.ClearMessage();
        UIManager.Instance.ShowCombatUI();
        UIManager.Instance.ShowBattleResult("Press F to Roll Dice");
    }

    private IEnumerator DiceRoutine()
    {
        if (diceClip != null)
            diceAudio.PlayOneShot(diceClip);

        if (diceClip != null)
            yield return new WaitForSeconds(diceClip.length);

        yield return new WaitForSeconds(diceResultDelay);

        ContinueBattle();
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

            if (enemyHP <= 0)
            {
                battleDone = true;
                UIManager.Instance.ShowBattleResult("YOU WIN! YOU GOT A SCORE! (Press F to close)");
                GameState.AddScore(1);
                WaitForPlayerClose();
            }
            else
            {
                UIManager.Instance.ShowBattleResult("Hit! Enemy -1 HP");
            }
        }
        else if (playerDice < enemyDice)
        {
            GameState.TakeDamage(1);
            battleDone = true;
            UIManager.Instance.ShowBattleResult("You took damage! -1 HP (Press F to close)");
            WaitForPlayerClose();
        }
        else
        {
            battleDone = true;
            UIManager.Instance.ShowBattleResult("Draw (Press F to close)");
            WaitForPlayerClose();
        }
    }

    private void WaitForPlayerClose()
    {
        waitingForClose = true;
        if (player != null)
            player.canMove = false;
    }

    private void CloseBattleMessage()
    {
        UIManager.Instance.HideBattleResult();
        UIManager.Instance.HideCombatUI();
        UIManager.Instance.ShowMessage("This room's battle is already cleared.");
        waitingForClose = false;

        if (player != null)
            player.canMove = true;
    }
}
