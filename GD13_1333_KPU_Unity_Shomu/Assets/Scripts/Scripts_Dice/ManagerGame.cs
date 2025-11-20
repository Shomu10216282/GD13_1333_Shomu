using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    [Header("References")]
    public MapGenerator mapGenerator;
    public GameObject playerPrefab;

    private void Start()
    {
        mapGenerator.GenerateMap();

        Room randomRoom = mapGenerator.generatedRooms[
            Random.Range(0, mapGenerator.generatedRooms.Count)];

        SpawnPlayer(randomRoom);
    }

    private void SpawnPlayer(Room room)
    {
        Vector3 start = room.transform.position + Vector3.up * 10f;

        Vector3 spawnPos = start;

        if (Physics.Raycast(start, Vector3.down, out RaycastHit hit, 50f))
        {
            spawnPos = hit.point + Vector3.up * 1f; 
        }
        else
        {
            Debug.LogWarning("Player spawn raycast did not hit ground.");
        }

        Instantiate(playerPrefab, spawnPos, Quaternion.identity);
    }
}
