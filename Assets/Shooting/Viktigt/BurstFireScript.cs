using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstFireScript : MonoBehaviour
{
    public int shots;
    [SerializeField]
    GameObject bullet;
    public int maxAmmo, currentAmmo;
    float fireCd = 0.07f, timeSinceLastShot = 0;
    [SerializeField]
    float damage;
    [SerializeField]
    Transform bulletSpawn;
    float shotSpeed = 50;
    Vector3 direction;
    GameObject target;
    bool shooting = false;
    int i = 0;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        direction = transform.forward;

    }
    public void Shoot()
    {

        //GameObject createBullet = GameObject.Instantiate(bullet);
        //Bullet firedBullet = createBullet.GetComponent<Bullet>();
        //firedBullet.GetValues(bulletSpawn, shotSpeed, direction, damage);
        if (!shooting)
        {

            StartCoroutine(Burst());
            shooting = true;
        }

    }

    private IEnumerator Burst()
    {
        shots = 0;
        bool bursting = true;
        float timeBetweenShots = 0.05f;
        float timeSinceLastShot = 0;

        do
        {
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot > timeBetweenShots)
            {
                currentAmmo --;
                GameObject createBullet = GameObject.Instantiate(bullet);
                Bullet firedBullet = createBullet.GetComponent<Bullet>();
                firedBullet.GetValues(bulletSpawn, shotSpeed, direction, damage);
                shots++;
                timeSinceLastShot = 0;
                FindObjectOfType<AudioManager>().Play("PistolShot");
                if (shots == 3)
                {
                    bursting = false;

                    shooting = false;
                }
            }
            Debug.Log(shots.ToString());
            yield return null;
        } while (bursting);

    }
}
