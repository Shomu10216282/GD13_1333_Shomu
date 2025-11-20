using System;
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
    public string roomName = "Base Room";

    public GameObject directionArrow;

    protected bool playerInside = false;

    protected virtual void Start()
    {
        Debug.Log(roomName + " initialized.");
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
            Debug.Log("Player entered " + roomName);
            OnPlayerEnter();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            Debug.Log("Player exited " + roomName);
            OnPlayerExit();
        }
    }

    public virtual void TriggerPlayerInteract()
    {
        ShowDirectionGuide();
    }

    protected void ShowDirectionGuide()
    {
        Room next = north ?? east ?? south ?? west;

        if (next == null)
        {
            Debug.Log("No exit room found.");
            return;
        }

        if (directionArrow != null)
        {
            directionArrow.transform.LookAt(next.transform.position);
            directionArrow.SetActive(true);

            Invoke(nameof(HideArrow), 3f);
        }

        Debug.Log("Next room: " + next.roomName);
    }

    private void HideArrow()
    {
        if (directionArrow != null)
            directionArrow.SetActive(false);
    }

    protected virtual void OnPlayerEnter() { }
    protected virtual void OnPlayerExit() { }

    internal void TriggerPlayerEnter()
    {
        throw new NotImplementedException();
    }
}
