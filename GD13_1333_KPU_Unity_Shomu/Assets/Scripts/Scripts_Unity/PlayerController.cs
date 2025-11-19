using UnityEngine;

public class PlayerController : MonoBehaviour
{
<<<<<<< Updated upstream
    public float moveSpeed = 5f;
=======
    [Header("Movement Settings")]
    public float moveSpeed = 4f;
    public float mouseSensitivity = 2f;

    private float cameraPitch = 0f;

    private Room currentRoom;
>>>>>>> Stashed changes

    [SerializeField] private Camera playerCamera;

    private void Start()
    {
<<<<<<< Updated upstream
=======
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
>>>>>>> Stashed changes

        Vector3 pos = transform.position;
        pos.y = 1f;
        transform.position = pos;
<<<<<<< Updated upstream
=======

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
>>>>>>> Stashed changes
    }

    void Update()
    {
<<<<<<< Updated upstream
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0, z);
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
=======
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
>>>>>>> Stashed changes

    private void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

<<<<<<< Updated upstream
        if (move != Vector3.zero)
        {
            transform.forward = move;
        }
    }
=======
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
            Debug.Log("Entered " + room.roomName);
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
>>>>>>> Stashed changes
}
