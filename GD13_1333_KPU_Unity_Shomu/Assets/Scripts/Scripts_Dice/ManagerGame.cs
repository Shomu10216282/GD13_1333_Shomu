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
        Collider floorCollider = room.GetComponentInChildren<Collider>();
        float floorY = room.transform.position.y;
        if (floorCollider != null)
        {
            floorY = floorCollider.bounds.max.y;
        }

        float playerHeightOffset = 1f; 
        Vector3 spawnPos = new Vector3(room.transform.position.x, floorY + playerHeightOffset, room.transform.position.z);

        Instantiate(playerPrefab, spawnPos, Quaternion.identity);
    }
}
