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
        float floorY = room.transform.position.y;

        Collider[] cols = room.GetComponentsInChildren<Collider>();
        foreach (Collider col in cols)
        {
            floorY = Mathf.Max(floorY, col.bounds.max.y);
        }

        Vector3 spawnPos = new Vector3(
            room.transform.position.x,
            floorY + 1f,
            room.transform.position.z
        );

        Instantiate(playerPrefab, spawnPos, Quaternion.identity);
    }
}
