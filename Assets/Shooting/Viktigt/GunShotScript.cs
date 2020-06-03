using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;

public class GunShotScript : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawn;
    public int maxAmmo, currentAmmo;
    public float fireCd, timeSinceLastShot = 0;
    [SerializeField]
    float shotSpeed = 50;
    Vector3 direction;
    GameObject target, hand;
    [SerializeField] public float damage;
    public bool isPlayer;
    //Hittar kulornas mål och beräknar riktning

    private void Start()
    {
    }

    void Update()
    {
        direction = transform.forward;
        target = GameObject.FindGameObjectWithTag("Target");
        // direction = target.transform.position - this.transform.position;
        // timeSinceLastShot += Time.deltaTime;
        ////Försöker avfyra en kula
        //if (Input.GetMouseButton(0) == true && timeSinceLastShot > fireCd)
        //{
        //    Shoot();
        //    timeSinceLastShot = 0;
        //    
        //}
    }
    //Om en kula kan avfyras skapas den och ges värden
    public void Shoot()
    {
        if (currentAmmo >= 1)
        {

            GameObject createBullet = GameObject.Instantiate(bullet);

            Bullet firedBullet = createBullet.GetComponent<Bullet>();
            firedBullet.GetValues(bulletSpawn.transform, shotSpeed, direction, damage);
            FindObjectOfType<AudioManager>().Play("PistolShot");
        }

        //FindObjectOfType<AudioManager>().Play("PistolShot");
    }
}