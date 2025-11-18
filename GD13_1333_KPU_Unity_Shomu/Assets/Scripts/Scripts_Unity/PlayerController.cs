using UnityEngine;

public class PlayerController : MonoBehaviour

{

    public float moveSpeed = 4f;

    public float rotateSpeed = 180f;

    private Room currentRoom;

    private void Start()

    {
        Vector3 pos = transform.position;

        pos.y = 1f;

        transform.position = pos;

    }

    void Update()

    {

        HandleMovement();

        HandleInteraction();

    }

    private void HandleMovement()

    {

        float h = Input.GetAxis("Horizontal");

        float v = Input.GetAxis("Vertical");


        Vector3 move = transform.forward * v * moveSpeed * Time.deltaTime;

        transform.position += move;


        transform.Rotate(Vector3.up * h * rotateSpeed * Time.deltaTime);

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

}
