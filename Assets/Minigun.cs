using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : MonoBehaviour
{
    public bool winding = false, shooting = false;
    [SerializeField] float windUpCounter = 0;
    [SerializeField] float velocity;
    float timeSinceLastShot = 0, fireCd = 0.04f, damage;
    [SerializeField] GameObject bullet, bulletSpawn;
    public FieldOfView fieldOfView;

    // Karl 

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (shooting && timeSinceLastShot > fireCd)
        {
            GameObject tempObject = GameObject.Instantiate(bullet);
            tempObject.GetComponent<Bullet>().GetValues(bulletSpawn.transform, velocity, transform.forward, damage);
            Bullet tempBullet = tempObject.GetComponent<Bullet>();
            tempBullet.transform.Rotate(new Vector3(0, Random.Range(-10.0f, 10.0f), 0));
            FindObjectOfType<AudioManager>().PlayOneShot("MiniGun");
            tempBullet.ChangeDirection(tempObject.transform.forward);
            timeSinceLastShot = 0;
        }
        if (fieldOfView.visableTargets.Count >= 1)
        {
            Shoot();
        }
        else
        {
            Stop();
        }
    }

    //Weapon winds up before firing
    private IEnumerator windUp()
    {
      
        do
        {
            if (windUpCounter < 1)
            {
                windUpCounter += Time.deltaTime;
            }


            if (windUpCounter >= 1)
            {
                shooting = true;
                winding = false;
            }
            yield return null;
        } while (winding);
    }
    //Weapon winds down after firing
    private IEnumerator windDown()
    {
        
        do
        {
            windUpCounter -= 0.016666f;
            if (windUpCounter < 1.5f)
            {
                shooting = false;
            }
            yield return null;
        } while (!winding && windUpCounter > 0);
        if (windUpCounter <= 0)
        {
            windUpCounter = 0;
        }
    }

    //Starts wind up
    public void Shoot()
    {
        if (!winding)
        {
            winding = true;
            StartCoroutine(windUp());
        }
    }
    //Starts wind down
    public void Stop()
    {
        if (winding)
        {
            winding = false;
            StartCoroutine(windDown());
        }
        else
        {
            winding = false;
            StartCoroutine(windDown());
        }
    }
}
