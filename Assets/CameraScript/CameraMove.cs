using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //Paul
    public GameObject player;
    float xaxis, zaxis, yaxis;
    bool moveRight, moveLeft, moveUp, moveDown;
    public int camSpeed = 2;
    public Vector3 offset;
    public Vector3 camAngle;
    // Update is called once per frame

    void Update()
    {
  
        xaxis = player.transform.position.x;
            zaxis = player.transform.position.z;
            yaxis = player.transform.position.y;
      
        transform.position = new Vector3(xaxis, yaxis + 20, zaxis) + offset;
        transform.rotation = Quaternion.Euler(camAngle);
    }
}
