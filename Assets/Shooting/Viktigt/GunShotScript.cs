using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShotScript : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawn;
    public int maxAmmo, currentAmmo;
    float fireCd = 0.2f, timeSinceLastShot = 0;
    [SerializeField]
    float shotSpeed = 50;
    Vector3 direction;
    GameObject target, hand;
    [SerializeField] float damage;
    public bool isPlayer;
    //Hittar kulornas mål och beräknar riktning

    void Update()
    {
        direction = transform.forward;
<<<<<<< HEAD
        
        //if (isPlayer)
        //{
        //    target = GameObject.FindGameObjectWithTag("Target");

=======

        //if (isPlayer)
        //{
        //    target = GameObject.FindGameObjectWithTag("Target");

>>>>>>> LevelDesign
        //    direction = target.transform.position - this.transform.position;
        //    timeSinceLastShot += Time.deltaTime;
        //    //Försöker avfyra en kula
        //    if (Input.GetMouseButton(0) == true && timeSinceLastShot > fireCd)
        //    {
        //        Shoot();
        //        timeSinceLastShot = 0;
        //    }
        //}
        //else
        //{
        //    direction = transform.forward;
        //}
    }
    //Om en kula kan avfyras skapas den och ges värden
    public void Shoot()
    {
        GameObject createBullet = GameObject.Instantiate(bullet);
        //JeffsBullets firedBullet = createBullet.GetComponent<JeffsBullets>();
        Bullet firedBullet = createBullet.GetComponent<Bullet>();
        firedBullet.GetValues(bulletSpawn.transform, shotSpeed, direction, damage);
    }
}