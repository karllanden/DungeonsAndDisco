using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunScript : MonoBehaviour
{
    //public GameObject bullet;
    AudioSource audio;
    [SerializeField] GameObject bullet;
    public int maxAmmo, currentAmmo;
    [SerializeField] float fireCd = 0.6f, damage;
    float timeSinceLastShot = 0;
    float shotSpeed = 30;
    Vector3 direction;
    GameObject target;
    [SerializeField] Transform bulletSpawn;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Target");

        //direction = target.transform.position - this.transform.position;
        //timeSinceLastShot += Time.deltaTime;

        //if (Input.GetMouseButton(0) == true && timeSinceLastShot > fireCd)
        //{
        //    Shoot(target);
        //    timeSinceLastShot = 0;
        //}
    }

    public void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("ShotGun");
        currentAmmo--;
        //Skapa 5 kulor       
        GameObject[] createBullets = new GameObject[5];
        for (int i = 0; i < 5; i++)
        {
            createBullets[i] = GameObject.Instantiate(bullet, this.direction, this.transform.rotation);
            Bullet tempObject = createBullets[i].GetComponent<Bullet>();
            tempObject.GetValues(bulletSpawn.transform, shotSpeed, direction, damage);
        }
        //Ge alla kulor en unik rikting inom en viss spridning från där man siktar
        float angle = 15;
        foreach (GameObject g in createBullets)
        {
            Bullet tempObject = g.GetComponent<Bullet>();
            tempObject.transform.Rotate(new Vector3(0, angle, 0));
            tempObject.ChangeDirection(tempObject.transform.forward);
            angle -= 7.5f;
        }
    }
}
