using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private void Start()
    {

        Vector3 pos = transform.position;
        pos.y = 1f;
        transform.position = pos;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0, z);
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);


        if (move != Vector3.zero)
        {
            transform.forward = move;
        }
    }
}
