using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : MonoBehaviour
{
    public bool winding = false, shooting = false;
    [SerializeField] float windUpCounter = 0;
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
            tempObject.GetComponent<Bullet>().GetValues(bulletSpawn.transform, 50, transform.forward, damage);
            Bullet tempBullet = tempObject.GetComponent<Bullet>();
            tempBullet.transform.Rotate(new Vector3(0, Random.Range(-10.0f, 10.0f), 0));
            tempBullet.ChangeDirection(tempObject.transform.forward);
            timeSinceLastShot = 0;
        }
        if (fieldOfView.visableTargets.Count >= 1)
        {
            Shoot();
        }
    }

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
    }

    public void Shoot()
    {
        if (!winding)
        {
            winding = true;
            StartCoroutine(windUp());
        }
    }
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
