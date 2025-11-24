using System.Collections.Generic;
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

    public List<Room> generatedRooms = new List<Room>();

    private Room[,] rooms;

    public void GenerateMap()
    {
        generatedRooms.Clear();
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

                if (prefab == treasureRoomPrefab)
                {
                    room.roomType = RoomType.Treasure;
                    room.roomName = "Treasure Room";
                }
                else if (prefab == combatRoomPrefab)
                {
                    room.roomType = RoomType.Combat;
                    room.roomName = "Combat Room";
                }
                else
                {
                    room.roomType = RoomType.Base;
                    room.roomName = "Base Room";
                }

                room.gridPosition = new Vector2Int(x, y);

                rooms[x, y] = room;
                generatedRooms.Add(room);
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
