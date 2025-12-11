using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    [Header("References")]
    public MapGenerator mapGenerator;
    public GameObject playerPrefab;

    [Header("Game Settings")]
    public int scoreToWin = 10;
    public int initialHP = 10;

    [Header("Spawn Settings")]
    public float spawnHeightOffset = 0.1f;

    private void Awake()
    {
        GameState.Initialize(initialHP, 0, scoreToWin, initialHP);
    }

    private void Start()
    {
        UIManager.Instance.SetMaxScore(scoreToWin);

        if (mapGenerator == null)
        {
            Debug.LogError("MapGenerator reference missing on ManagerGame!");
            return;
        }

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
        float floorY = room.transform.position.y;

        Vector3 spawnPos = new Vector3(
            room.transform.position.x,
            floorY + spawnHeightOffset,
            room.transform.position.z
        );

        Instantiate(playerPrefab, spawnPos, Quaternion.identity);
    }

    private void CheckWinCondition(int newScore)
    {
        Debug.Log($"Score changed: {newScore}/{scoreToWin}");
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
