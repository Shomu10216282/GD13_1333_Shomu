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

    [Header("Doors")]
    public GameObject northDoor;
    public GameObject southDoor;
    public GameObject eastDoor;
    public GameObject westDoor;
    public float openDistance = 1f;

    protected bool playerInside = false;
    protected Transform playerTransform;

    protected virtual void Start()
    {
        if (directionArrow != null)
            directionArrow.SetActive(false);

        if (northDoor != null) northDoor.SetActive(true);
        if (southDoor != null) southDoor.SetActive(true);
        if (eastDoor != null) eastDoor.SetActive(true);
        if (westDoor != null) westDoor.SetActive(true);
    }

    private void Update()
    {
        HandleDoors();
    }

    private void HandleDoors()
    {
        if (!playerInside || playerTransform == null) return;

        OpenOrCloseDoor(northDoor);
        OpenOrCloseDoor(southDoor);
        OpenOrCloseDoor(eastDoor);
        OpenOrCloseDoor(westDoor);
    }

    private void OpenOrCloseDoor(GameObject door)
    {
        if (door == null) return;

        float distance = Vector3.Distance(playerTransform.position, door.transform.position);
        door.SetActive(distance > openDistance); 
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            playerTransform = other.transform;
            OnPlayerEnter();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            playerTransform = null;

            if (northDoor != null) northDoor.SetActive(true);
            if (southDoor != null) southDoor.SetActive(true);
            if (eastDoor != null) eastDoor.SetActive(true);
            if (westDoor != null) westDoor.SetActive(true);

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
            return;

        if (directionArrow != null)
        {
            directionArrow.transform.LookAt(next.transform.position);
            directionArrow.SetActive(true);
            Invoke(nameof(HideArrow), 3f);
        }
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
