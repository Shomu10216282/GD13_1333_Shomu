using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    [SerializeField] private float MovementTime = 2.0f;
    private bool _isMoving = false;
    private float _movementTimer = 0f;
    private Vector3 _previousPosition;
    private Vector3 _moveToPosition;
    // Change the type of _currentRoom from object to RoomBase
    private RoomBase _currentRoom;

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

        if (_isMoving)
        {
            Vector3 currentPosition = Vector3.Slerp(_previousPosition, _moveToPosition, _movementTimer / MovementTime);
            transform.position = currentPosition;
            _movementTimer += Time.deltaTime;
            if (_movementTimer >= MovementTime)
            {
                _isMoving = false;
                _movementTimer = 0.0f;
                transform.position = _moveToPosition;
            }
        }
        else
        {
            bool rotateLeft = Input.GetKeyDown(KeyCode.A);
            bool rotateRight = Input.GetKeyDown(KeyCode.D);

            if (rotateLeft && !rotateRight)
            {
                TurnLeft();
            }
            else if (rotateRight && !rotateLeft)
            {
                TurnRight();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if(_currentRoom != null)
                {
                    _currentRoom.OnRoomSearched();
                }
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                RoomBase roomInFacingDirection = NextRoomInDirection();
                if (roomInFacingDirection != null)
                {
                    StartMovement(roomInFacingDirection);
                }
            }
        }

    }

    private void StartMovement(RoomBase roomInFacingDirection)
    {
        throw new NotImplementedException();
    }

    private RoomBase NextRoomInDirection()
    {
        throw new NotImplementedException();
    }

    private void TurnRight()
    {
        throw new NotImplementedException();
    }

    private void TurnLeft()
    {
        throw new NotImplementedException();
    }
}
