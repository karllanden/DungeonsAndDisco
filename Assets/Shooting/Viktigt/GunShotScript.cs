using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShotScript : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawn;
    public int maxAmmo, currentAmmo;
    float fireCd = 0.2f, timeSinceLastShot = 0;
    float shotSpeed = 50;
    Vector3 direction;
    GameObject target, hand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //if(hand != null)
       //{
            target = GameObject.FindGameObjectWithTag("Target");

            direction = target.transform.position - this.transform.position;
            timeSinceLastShot += Time.deltaTime;

            if (Input.GetMouseButton(0) == true && timeSinceLastShot > fireCd)
            {
                Shoot();
                timeSinceLastShot = 0;
            }
      // }
       

    }

    void Shoot()
    {
        GameObject createBullet = GameObject.Instantiate(bullet);
        Bullet firedBullet = createBullet.GetComponent<Bullet>();
        firedBullet.GetValues(bulletSpawn.transform, shotSpeed, direction);
    }
}