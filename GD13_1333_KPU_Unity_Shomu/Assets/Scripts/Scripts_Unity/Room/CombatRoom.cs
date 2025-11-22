using UnityEngine;
using System.Collections.Generic;

public class CombatRoom : Room
{
    [Header("Combat Settings")]
    public GameObject door;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    private bool combatStarted = false;
    private bool combatFinished = false;

    private List<GameObject> enemies = new List<GameObject>();
    private Room previousRoom;

    protected override void Start()
    {
        roomName = "Combat Room";
        base.Start();
    }

    protected override void OnPlayerEnter()
    {
        if (!combatStarted)
        {
            combatStarted = true;
            combatFinished = false;
            StartCombat();
        }
    }

    private void StartCombat()
    {
        Debug.Log("Combat Started!");

        if (door != null)
            door.SetActive(true);

        enemies.Clear();

        foreach (Transform t in spawnPoints)
        {
            GameObject e = Instantiate(enemyPrefab, t.position, Quaternion.identity);
            enemies.Add(e);

            Enemy enemyComponent = e.GetComponent<Enemy>();
            enemyComponent.onDeath = OnEnemyDead;
        }
    }

    private void OnEnemyDead(GameObject enemy)
    {
        enemies.Remove(enemy);

        if (enemies.Count == 0)
        {
            EndCombat();
        }
    }

    private void EndCombat()
    {
        combatFinished = true;

        Debug.Log("Combat Cleared!");

        if (door != null)
            door.SetActive(false);
    }

    public void SetPreviousRoom(Room room)
    {
        previousRoom = room;
    }

    public override void TriggerPlayerInteract()
    {
        if (!combatFinished)
        {
            DiceBattle();
            return;
        }

        base.TriggerPlayerInteract();
    }

    private void DiceBattle()
    {
        int playerRoll = Random.Range(1, 7);
        int enemyRoll = Random.Range(1, 7);

        Debug.Log($"Player rolled {playerRoll} / Enemy rolled {enemyRoll}");

        if (playerRoll >= enemyRoll)
        {
            Debug.Log("You win the dice battle!");
            DamageEnemyByDice();
        }
        else
        {
            Debug.Log("You lose the dice battle. Taking 1 damage...");
            PlayerStats.TakeDamage(1);

            ReturnPlayerToPreviousRoom();
        }
    }

    private void DamageEnemyByDice()
    {
        if (enemies.Count == 0) return;

        GameObject targetEnemy = enemies[0];
        enemies.Remove(targetEnemy);

        Destroy(targetEnemy);

        if (enemies.Count == 0)
            EndCombat();
    }

    private void ReturnPlayerToPreviousRoom()
    {
        if (previousRoom == null)
        {
            Debug.Log("Previous room not set!");
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 pos = previousRoom.transform.position + Vector3.up * 1.5f;
            player.transform.position = pos;

            Debug.Log("Player returned to previous room after losing.");
        }
    }
}
