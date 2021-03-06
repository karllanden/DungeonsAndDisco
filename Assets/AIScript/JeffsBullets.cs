﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeffsBullets : MonoBehaviour
{
    float velocity, damage;
    //Obsolete
    Vector3 direction;
    //räknar ut hur långt kulan ska färdas denna frame
    void Update()
    {


        float distanceThisFrame = velocity * Time.deltaTime;
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);

    }

    //bestämmer när en kollision händer och sedan hur kulan ska agera
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            Destroy(this.gameObject);
        }
        if (other.tag != "Gun")
        {
            Destroy(this.gameObject);
        }
        if (other.tag == "Player")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerHealthScript>().takeDamage(damage);

        }
    }
    //tar emot värden som bestämmer kulans "behavior" från vapnet
    public void GetValues(Transform tf, float v, Vector3 dir, float damage)
    {
        this.transform.rotation = tf.rotation;
        this.transform.position = tf.position;
        velocity = v;
        direction = dir;
    }
}
