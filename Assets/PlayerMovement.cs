using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public int movementSpeed;
    bool moveLeft, moveRight, moveUp, moveDown;

    void MoveInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveLeft = true;
        }
        else
        {
            moveLeft = false;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDown = true;
        }
        else
        {
            moveDown = false;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveRight = true;
        }
        else
        {
            moveRight = false;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveUp = true;
        }
        else
        {
            moveUp = false;
        }
    }
    void FixedUpdate()
    {
        MoveInput();
        if (moveLeft)
        {
            rb.AddForce(-movementSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if (moveRight)
        {
            rb.AddForce(movementSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if (moveUp)
        {
            rb.AddForce(0, 0, movementSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
        if (moveDown)
        {
            rb.AddForce(0, 0, -movementSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
        if (!moveDown && !moveLeft && !moveRight && !moveUp)
        {
            rb.velocity = new Vector3(0, -8, 0);
        }
    }
}
