using UnityEngine;
using System.Collections.Generic;

public class CombatRoom : Room
{
    public GameObject door;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    private bool combatStarted = false;
    private List<GameObject> enemies = new List<GameObject>();

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
            StartCombat();
        }
    }

    private void StartCombat()
    {
        Debug.Log("Combat Started!");

        if (door != null)
            door.SetActive(true);

        foreach (Transform t in spawnPoints)
        {
            GameObject e = Instantiate(enemyPrefab, t.position, Quaternion.identity);
            enemies.Add(e);
            e.GetComponent<Enemy>().onDeath = OnEnemyDead;
        }
    }

    private void OnEnemyDead(GameObject enemy)
    {
        enemies.Remove(enemy);

        if (enemies.Count == 0)
            EndCombat();
    }

    private void EndCombat()
    {
        Debug.Log("Combat Cleared!");

        if (door != null)
            door.SetActive(false);
    }

    public override void TriggerPlayerInteract()
    {
        base.TriggerPlayerInteract();
    }
}
