using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Room Connections")]
    public Room north;
    public Room south;
    public Room east;
    public Room west;

    [Header("Room Info")]
    public Vector2Int gridPosition;


    public enum RoomType { Base, Treasure, Combat }
    public RoomType roomType = RoomType.Base;

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        Vector3 pos = transform.position;

        if (north) Gizmos.DrawLine(pos, north.transform.position);
        if (south) Gizmos.DrawLine(pos, south.transform.position);
        if (east) Gizmos.DrawLine(pos, east.transform.position);
        if (west) Gizmos.DrawLine(pos, west.transform.position);
    }


    //Assignment 3
    public string roomName = "Base Room";

    protected virtual void Start()
    {
        Debug.Log(roomName + " initialized.");
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered " + roomName);
            OnPlayerEnter();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited " + roomName);
            OnPlayerExit();
        }
    }

    protected virtual void OnPlayerEnter() { }
    protected virtual void OnPlayerExit() { }

}
