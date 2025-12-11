using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 4f;
    public float rotationSpeed = 120f;

    [Header("Audio Settings")]
    public AudioClip walkClip;
    private AudioSource walkAudio;
    private float walkInterval = 0.4f;
    private float walkTimer = 0f;

    private Rigidbody rb;
    private Room currentRoom;
    public bool canMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        walkAudio = gameObject.AddComponent<AudioSource>();
        walkAudio.clip = walkClip;
        walkAudio.playOnAwake = false;
        walkAudio.loop = false;
        walkAudio.volume = 1f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (UIManager.Instance.gameClearPanel.activeSelf ||
            UIManager.Instance.gameOverPanel.activeSelf)
            return;

        if (Input.GetKeyDown(KeyCode.Tab))
            UIManager.Instance.TogglePauseUI();

        if (UIManager.Instance.pausePanel.activeSelf)
            return;

        if (UIManager.Instance.gameClearPanel.activeSelf ||
            UIManager.Instance.gameOverPanel.activeSelf)
            return;

        if (Input.GetKeyDown(KeyCode.F) && currentRoom != null)
            currentRoom.TriggerPlayerInteract();
    }

    private void FixedUpdate()
    {
        if (!canMove ||
            UIManager.Instance.isGameFrozen ||
            UIManager.Instance.gameClearPanel.activeSelf ||
            UIManager.Instance.gameOverPanel.activeSelf)
        {
            StopWalkAudio();
            return;
        }

        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        walkTimer -= Time.fixedDeltaTime;

        float forward = 0f;
        if (Input.GetKey(KeyCode.W)) forward += 1f;
        if (Input.GetKey(KeyCode.S)) forward -= 1f;

        bool isMoving = forward != 0;
        Vector3 moveDir = transform.forward * forward;

        if (isMoving)
        {
            if (walkTimer <= 0f && walkClip != null)
            {
                walkAudio.PlayOneShot(walkClip);
                walkTimer = walkInterval;
            }

            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            StopWalkAudio();
        }
    }

    private void StopWalkAudio()
    {
        if (walkAudio != null && walkAudio.isPlaying)
            walkAudio.Stop();
    }

    private void HandleRotation()
    {
        float rotation = 0f;
        if (Input.GetKey(KeyCode.D)) rotation += 1f;
        if (Input.GetKey(KeyCode.A)) rotation -= 1f;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(
            0f,
            rotation * rotationSpeed * Time.fixedDeltaTime,
            0f
        ));
    }

    private void OnTriggerEnter(Collider other)
    {
        Room room = other.GetComponent<Room>();
        if (room != null) currentRoom = room;
    }

    private void OnTriggerExit(Collider other)
    {
        Room room = other.GetComponent<Room>();
        if (room == currentRoom) currentRoom = null;
    }
}
