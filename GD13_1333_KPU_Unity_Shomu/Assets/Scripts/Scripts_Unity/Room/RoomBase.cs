using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class RoomBase : MonoBehaviour 
{
    [SerializeField] private GameObject NorthDoorway, SouthDoorway, EastDoorway, WestDoorway;
    private RoomBase _east;
    private RoomBase _north;
    private RoomBase _south;
    private RoomBase _west;

    public void SetRooms(RoomBase roomNorth, RoomBase roomEast, RoomBase roomSouth, RoomBase roomWest)
    {
        _north = roomNorth;
        NorthDoorway.SetActive(_north == null);
        _east = roomEast;
        EastDoorway.SetActive(_east == null);
        _south = roomSouth;
        SouthDoorway.SetActive(_south == null);
        _west = roomWest;
        WestDoorway.SetActive(_west == null);
    }
}
