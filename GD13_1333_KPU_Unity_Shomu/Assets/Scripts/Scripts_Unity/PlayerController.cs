using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 4f;
    public float mouseSensitivity = 2f;

    private float cameraPitch = 0f;
    private Room currentRoom;

    [SerializeField] private Camera playerCamera;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerCamera == null)
            playerCamera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        HandleMovement();
        HandleLook();
        HandleInteraction();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDir = transform.right * x + transform.forward * z;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
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
