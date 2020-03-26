﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCC : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12;
    public float startSpeed = 12;

    Vector3 x = new Vector3(1,1,1);



    void Update()
    {


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(1,0,0) * x + new Vector3(0, 0, 1) * z;

        controller.Move(move * speed * Time.deltaTime);


    }
}

