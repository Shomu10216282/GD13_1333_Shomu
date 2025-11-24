using UnityEngine;

public enum RoomType
{
    Base,
    Treasure,
    Combat
}

public class Room : MonoBehaviour
{
    [Header("Room Connections")]
    public Room north;
    public Room south;
    public Room east;
    public Room west;

    [Header("Room Info")]
    public Vector2Int gridPosition;
    public RoomType roomType;
    public string roomName = "Base Room";

    [Header("Direction Arrow")]
    public GameObject directionArrow;

    protected bool playerInside = false;


    protected virtual void Start()
    {
        Debug.Log(roomName + " initialized.");
        if (directionArrow != null)
            directionArrow.SetActive(false);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 pos = transform.position;

        if (north) Gizmos.DrawLine(pos, north.transform.position);
        if (south) Gizmos.DrawLine(pos, south.transform.position);
        if (east) Gizmos.DrawLine(pos, east.transform.position);
        if (west) Gizmos.DrawLine(pos, west.transform.position);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            OnPlayerEnter();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            HideArrow();
            OnPlayerExit();
        }
    }


    public virtual void TriggerPlayerInteract()
    {
        ShowDirectionGuide();
    }


    protected void ShowDirectionGuide()
    {
        Room next = GetNextRoom();

        if (next == null)
        {
            Debug.Log("No room to go");
            return;
        }

        if (directionArrow != null)
        {
            directionArrow.transform.LookAt(next.transform.position);
            directionArrow.SetActive(true);
            Invoke(nameof(HideArrow), 3f);
        }

        Debug.Log("Next Room: " + next.roomName);
    }

    protected void HideArrow()
    {
        if (directionArrow != null)
            directionArrow.SetActive(false);
    }


    public Room GetNextRoom()
    {
        if (north != null) return north;
        if (east != null) return east;
        if (south != null) return south;
        if (west != null) return west;

        return null;
    }


    protected virtual void OnPlayerEnter() { }
    protected virtual void OnPlayerExit() { }
}
