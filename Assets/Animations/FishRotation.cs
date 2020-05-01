﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishRotation : MonoBehaviour
{
    [SerializeField]
    public Transform target;

    [SerializeField]
    int speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);
    }
}
