using GD13_1333_Shomu.Scripts;
using UnityEngine;

public class DoorController : MonoBehaviour

{

    [Header("Door Parts")]

    public GameObject doorModel;           

    private Collider doorCollider;

    [Header("Detection Settings")]

    public float openDistance = 3f;        

    public float viewAngle = 40f;          

    public Transform player;

    public Canvas hintCanvas;              

    private bool isOpen = false;

    void Start()

    {

        doorCollider = doorModel.GetComponent<Collider>();

        hintCanvas.enabled = false;

    }

    void Update()

    {

        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);

        Vector3 dirToDoor = (transform.position - player.position).normalized;

        float angle = Vector3.Angle(player.forward, dirToDoor);


        if (angle < viewAngle && distance < openDistance * 2f)

        {

            hintCanvas.enabled = true;

        }

        else

        {

            hintCanvas.enabled = false;

        }


        if (!isOpen && distance < openDistance)

        {

            OpenDoor();

        }


        if (isOpen && distance > openDistance + 2f)

        {

            CloseDoor();

        }

    }

    private void OpenDoor()

    {

        isOpen = true;


        if (doorCollider != null)

            doorCollider.enabled = false;


        doorModel.SetActive(false);

        Debug.Log("Door opened");

    }

    private void CloseDoor()

    {

        isOpen = false;


        if (doorCollider != null)

            doorCollider.enabled = true;

        doorModel.SetActive(true);

        Debug.Log("Door closed");

    }

}
