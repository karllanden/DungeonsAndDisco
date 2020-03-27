using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float velocity;
    //float aliveTime = 0;
    Vector3 direction;
    
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //aliveTime += Time.deltaTime;
        //if (aliveTime > 5)
        //    Destroy(gameObject);

        float distanceThisFrame = velocity * Time.deltaTime;
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        
    }

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
        
    }
    public void GetValues(Transform tf, float v, Vector3 dir)
    {
        this.transform.rotation = tf.rotation;
        this.transform.position = tf.position;
        velocity = v;
        direction = dir;
    }
}
