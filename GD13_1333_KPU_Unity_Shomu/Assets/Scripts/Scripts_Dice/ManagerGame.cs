using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    [Header("References")]
    public MapGenerator mapGenerator;
    public GameObject playerPrefab;

    [Header("Game Settings")]
    public int scoreToWin = 5;

    private void Start()
    {
        mapGenerator.GenerateMap();

        if (mapGenerator.generatedRooms == null || mapGenerator.generatedRooms.Count == 0)
        {
            Debug.LogError("No rooms generated! Check MapGenerator settings.");
            return;
        }

        Room randomRoom = mapGenerator.generatedRooms[
            Random.Range(0, mapGenerator.generatedRooms.Count)];

        SpawnPlayer(randomRoom);

        GameState.OnScoreChanged += CheckWinCondition;
    }

    private void SpawnPlayer(Room room)
    {
        if (room == null)
        {
            Debug.LogError("SpawnPlayer received a null room!");
            return;
        }

        float floorY = room.transform.position.y;

        Collider[] cols = room.GetComponentsInChildren<Collider>();
        foreach (Collider col in cols)
        {
            if (col.bounds.max.y > floorY)
                floorY = col.bounds.max.y;
        }

        Vector3 spawnPos = room.transform.position + new Vector3(0f, 1f, 0f);

        Instantiate(playerPrefab, spawnPos, Quaternion.identity);
    }

    private void CheckWinCondition(int newScore)
    {
        if (newScore >= scoreToWin)
        {
            Debug.Log("Game Cleared! You found all treasures!");
        }
    }

    private void OnDestroy()
    {
        GameState.OnScoreChanged -= CheckWinCondition;
    }
}
