using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstFireScript : MonoBehaviour
{
    public int shots;
    AudioSource audio;
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
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

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
                GameObject createBullet = GameObject.Instantiate(bullet);
                Bullet firedBullet = createBullet.GetComponent<Bullet>();
                firedBullet.GetValues(bulletSpawn, shotSpeed, direction, damage);
                shots++;
                timeSinceLastShot = 0;
                AudioSource newAudio = audio;
                newAudio.pitch = audio.pitch;
                newAudio.volume = audio.volume;
                newAudio.PlayOneShot(audio.clip);
                //GameObject[] createBullets = new GameObject[5];
                //for (int i = 0; i < 5; i++)
                //{
                //    createBullets[i] = GameObject.Instantiate(bullet, this.direction, this.transform.rotation);
                //    Bullet tempObject = createBullets[i].GetComponent<Bullet>();
                //    tempObject.GetValues(bulletSpawn.transform, shotSpeed, direction, damage);
                //}
                ////Ge alla kulor en unik rikting inom en viss spridning från där man siktar
                //float angle = 15;
                //foreach (GameObject g in createBullets)
                //{
                //    Bullet tempObject = g.GetComponent<Bullet>();
                //    tempObject.transform.Rotate(new Vector3(0, angle, 0));
                //    tempObject.ChangeDirection(tempObject.transform.forward);
                //    angle -= 7.5f;
                //}
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
