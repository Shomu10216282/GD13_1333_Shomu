using GD13_1333_Shomu.Scripts;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Room Prefabs")]
    public GameObject baseRoomPrefab;
    public GameObject treasureRoomPrefab;
    public GameObject combatRoomPrefab;

    [Header("Map Settings")]
    public int mapWidth = 3;
    public int mapHeight = 3;
    public float roomSpacing = 10f;

    private Room[,] rooms;

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        rooms = new Room[mapWidth, mapHeight];

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                float rand = Random.value;
                GameObject prefab;

                if (rand > 0.85f)
                    prefab = treasureRoomPrefab;  
                else if (rand > 0.45f)
                    prefab = combatRoomPrefab;    
                else
                    prefab = baseRoomPrefab;     


                Vector3 position = new Vector3(x * roomSpacing, 0, y * roomSpacing);
                GameObject roomObj = Instantiate(prefab, position, Quaternion.identity, transform);
                Room room = roomObj.GetComponent<Room>();
                room.gridPosition = new Vector2Int(x, y);


                if (prefab == treasureRoomPrefab)
                    room.roomType = Room.RoomType.Treasure;
                else if (prefab == combatRoomPrefab)
                    room.roomType = Room.RoomType.Combat;
                else
                    room.roomType = Room.RoomType.Base;

                rooms[x, y] = room;
            }
        }


        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                Room room = rooms[x, y];
                if (room == null) continue;

                if (y < mapHeight - 1) room.north = rooms[x, y + 1];
                if (y > 0) room.south = rooms[x, y - 1];
                if (x < mapWidth - 1) room.east = rooms[x + 1, y];
                if (x > 0) room.west = rooms[x - 1, y];
            }
        }
    }
}
