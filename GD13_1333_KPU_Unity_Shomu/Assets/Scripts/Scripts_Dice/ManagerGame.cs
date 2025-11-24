using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    [Header("References")]
    public MapGenerator mapGenerator;
    public GameObject playerPrefab;

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

        Vector3 spawnPos = new Vector3(
            room.transform.position.x,
            floorY + 1f, 
            room.transform.position.z
        );

        GameObject playerObj = Instantiate(playerPrefab, spawnPos, Quaternion.identity);

        Rigidbody rb = playerObj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
