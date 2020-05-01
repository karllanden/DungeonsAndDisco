using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstFireScript : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    public int maxAmmo, currentAmmo;
    float fireCd = 0.07f, timeSinceLastShot = 0;
    [SerializeField] float damage;
    [SerializeField] Transform bulletSpawn;
    float shotSpeed = 15;
    Vector3 direction;
    GameObject target;
    bool burst = false;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null)
        {
            target = GameObject.FindGameObjectWithTag("Target");

            direction = target.transform.position - this.transform.position;
            timeSinceLastShot += Time.deltaTime;

            if (Input.GetMouseButton(0) == true && timeSinceLastShot > fireCd && !burst)
            {
                i = 0;
                burst = true;
            }
            else if (burst && timeSinceLastShot > fireCd)
            {
                Shoot();
                i++;
                if (i == 3)
                {
                    timeSinceLastShot = -1f;
                    burst = false;
                }
                else
                {
                    timeSinceLastShot = 0;
                }



            }
        }
    }
    public void Shoot()
    {

        GameObject createBullet = GameObject.Instantiate(bullet);
        Bullet firedBullet = createBullet.GetComponent<Bullet>();
        firedBullet.GetValues(bulletSpawn, shotSpeed, direction, damage);
    }
}
