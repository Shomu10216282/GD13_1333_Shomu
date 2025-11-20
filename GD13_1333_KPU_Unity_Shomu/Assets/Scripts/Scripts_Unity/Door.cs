using UnityEngine;

public class Door : MonoBehaviour
{
    public float openHeight = 3f;      
    public float openSpeed = 3f;       
    public bool isOpen = false;

    private Vector3 closedPos;
    private Vector3 openPos;

    private void Start()
    {
        closedPos = transform.position;
        openPos = transform.position + Vector3.up * openHeight;
    }

    private void Update()
    {
        if (isOpen)
        {
            transform.position = Vector3.Lerp(transform.position, openPos, Time.deltaTime * openSpeed);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, closedPos, Time.deltaTime * openSpeed);
        }
    }

    public void OpenDoor()
    {
        isOpen = true;
    }

    public void CloseDoor()
    {
        isOpen = false;
    }
}
