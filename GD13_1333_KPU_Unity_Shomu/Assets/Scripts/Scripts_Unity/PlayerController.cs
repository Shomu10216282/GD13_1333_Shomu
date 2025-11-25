using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 4f;
    public float rotationSpeed = 120f; 

    private Rigidbody rb;
    private Room currentRoom;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        HandleInteraction();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        float forward = 0f;

        if (Input.GetKey(KeyCode.W)) forward += 1f;
        if (Input.GetKey(KeyCode.S)) forward -= 1f;

        Vector3 moveDir = transform.forward * forward;
        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    private void HandleRotation()
    {
        float rotation = 0f;

        if (Input.GetKey(KeyCode.D)) rotation += 1f;
        if (Input.GetKey(KeyCode.A)) rotation -= 1f;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rotation * rotationSpeed * Time.fixedDeltaTime, 0f));
    }

    private void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.F) && currentRoom != null)
        {
            currentRoom.TriggerPlayerInteract();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Room room = other.GetComponent<Room>();
        if (room != null)
            currentRoom = room;
    }

    private void OnTriggerExit(Collider other)
    {
        Room room = other.GetComponent<Room>();
        if (room != null && room == currentRoom)
            currentRoom = null;
    }
}
