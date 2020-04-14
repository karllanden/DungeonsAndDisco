using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterShotScript : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float damage;
    float velocity;
    float aliveTime = 0;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        Split();
        Destroy(gameObject);
    }

    public void ChangeDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    void Update()
    {
        aliveTime += Time.deltaTime;
        if (aliveTime > 5)
            Destroy(gameObject);

        float distanceThisFrame = velocity * Time.deltaTime;
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void Split()
    {
        GameObject[] createBullets = new GameObject[5];
        for (int i = 0; i < 5; i++)
        {
            createBullets[i] = GameObject.Instantiate(bullet, this.direction, this.transform.rotation);
            Bullet tempObject = createBullets[i].GetComponent<Bullet>();
            tempObject.GetValues(transform, velocity, direction, damage);
        }
        //Ge alla kulor en unik rikting inom en viss spridning från där man siktar
        float angle = 5;
        foreach (GameObject g in createBullets)
        {
            Bullet tempObject = g.GetComponent<Bullet>();
            tempObject.transform.Rotate(new Vector3(0, angle, 0));
            tempObject.ChangeDirection(tempObject.transform.forward);
            angle -= 2.5f;

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
