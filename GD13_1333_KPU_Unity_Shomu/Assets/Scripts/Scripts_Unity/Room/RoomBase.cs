using UnityEngine;

public class RoomBase : MonoBehaviour
{
    [Header("Doorways")]
    [SerializeField] private GameObject NorthDoorway;
    [SerializeField] private GameObject SouthDoorway;
    [SerializeField] private GameObject EastDoorway;
    [SerializeField] private GameObject WestDoorway;

    private RoomBase _north;
    private RoomBase _south;
    private RoomBase _east;
    private RoomBase _west;

    public void SetRooms(RoomBase roomNorth, RoomBase roomEast, RoomBase roomSouth, RoomBase roomWest)
    {
        _north = roomNorth;
        _east = roomEast;
        _south = roomSouth;
        _west = roomWest;

        SetupDoor(NorthDoorway, _north);
        SetupDoor(EastDoorway, _east);
        SetupDoor(SouthDoorway, _south);
        SetupDoor(WestDoorway, _west);
    }
    private void SetupDoor(GameObject door, RoomBase adjacentRoom)
    {
        if (door == null) return;

        door.SetActive(true);

        Collider col = door.GetComponent<Collider>();
        if (col == null)
            col = door.GetComponentInChildren<Collider>();

        if (col != null)
        {
            col.enabled = (adjacentRoom == null);
        }
    }
}
