using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCC : MonoBehaviour
{
    // Hampus 

    public CharacterController controller;
    public float speed = 12;
    public float startSpeed = 12;

    Vector3 x = new Vector3(1,1,1);



    void FixedUpdate()
    {

        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(-1,0,0) * x + new Vector3(0, 0, -1) * z;

        //uppdaterar förskjutning i x- och y-led i förhållande till angiven hastighet och Time.delaTime
        controller.Move(move * speed * Time.deltaTime);


    }
}

