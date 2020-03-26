using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveAmmo : MonoBehaviour
{
    float velocity;
    Vector3 direction;
    float explosionRadius = 5, damage = 10;
    

    // Update is called once per frame
    void Update()
    {
        float distanceThisFrame = velocity * Time.deltaTime;
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        for(int i =0; i < allEnemies.Length; i++)
        {
            if(allEnemies.Length == 0)
            {
                break;
            }
            Vector3 distance = allEnemies[i].transform.position - this.transform.position;
            if(distance.magnitude < explosionRadius)
            {
                //take damage metod här för fiender##
            }
        }
        Vector3 distanceToPlayer = player.transform.position - this.transform.position;
        if(distanceToPlayer.magnitude < explosionRadius)
        {
            //take damage metod här för spelar objektet
        }

        Destroy(this.gameObject);
    }
    public void GetValues(Transform tf, float v, Vector3 dir)
    {
        this.transform.rotation = tf.rotation;
        this.transform.position = tf.position;
        velocity = v;
        direction = dir;
    }
}
