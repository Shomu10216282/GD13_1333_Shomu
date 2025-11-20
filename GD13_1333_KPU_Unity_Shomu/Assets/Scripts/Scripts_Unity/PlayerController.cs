using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 4f;
    public float mouseSensitivity = 2f;

    private float cameraPitch = 0f;
    private Room currentRoom;

    [SerializeField] private Camera playerCamera;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>();
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleLook();
        HandleInteraction();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    private void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -45f, 70f);
        playerCamera.transform.localEulerAngles = new Vector3(cameraPitch, 0f, 0f);
    }

    private void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentRoom != null)
        {
            Debug.Log("Player searching room...");
            currentRoom.TriggerPlayerEnter();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Room room = other.GetComponent<Room>();
        if (room != null)
        {
            currentRoom = room;

            Vector3 center = room.transform.position;
            center.y = transform.position.y; 
            controller.enabled = false;     
            transform.position = center;
            controller.enabled = true;

            Debug.Log("Entered room → snapped to center: " + room.roomName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Room room = other.GetComponent<Room>();
        if (room != null && room == currentRoom)
        {
            currentRoom = null;
            Debug.Log("Exited " + room.roomName);
        }
    }
}
