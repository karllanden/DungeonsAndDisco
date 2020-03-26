using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    float xaxis, zaxis, yaxis;
    bool moveRight, moveLeft, moveUp, moveDown;
    public int camSpeed = 2;
    public Vector3 offset;
    public Vector3 camAngle;
    // Update is called once per frame
    private void KeyInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            moveLeft = true;
        }
        else
        {
            moveLeft = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveRight = true;
        }
        else
        {
            moveRight = false;
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveUp = true;
        }
        else
        {
            moveUp = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDown = true;
        }
        else
        {
            moveDown = false;
        }
    }
    void Update()
    {
        KeyInput();
        if (moveLeft)
        {
            xaxis -= camSpeed * Time.deltaTime;
        }
        if (moveRight)
        {
            xaxis += camSpeed * Time.deltaTime;
        }
        if (moveUp)
        {
            zaxis += camSpeed * Time.deltaTime;
        }
        if (moveDown)
        {
            zaxis -= camSpeed * Time.deltaTime;
        }
        if (!moveDown && !moveLeft && !moveRight && !moveUp)
        {
            xaxis = player.transform.position.x;
            zaxis = player.transform.position.z;
            yaxis = player.transform.position.y;
        }
        transform.position = new Vector3(xaxis, yaxis + 20, zaxis) + offset;
        transform.rotation = Quaternion.Euler(camAngle);
    }
}
