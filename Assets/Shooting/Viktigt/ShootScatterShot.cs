using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScatterShot : MonoBehaviour
{
    [SerializeField] GameObject scatterShot;
    public int maxAmmo, currentAmmo;
    [SerializeField] float fireCd = 0.3f;
    float timeSinceLastShot = 0;
    float shotSpeed = 15;
    Vector3 direction;
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Target");

        direction = target.transform.position - this.transform.position;
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetMouseButton(0) == true && timeSinceLastShot > fireCd)
        {
            Shoot();
            timeSinceLastShot = 0;
        }
    }

    void Shoot()
    {
        GameObject createBullet = GameObject.Instantiate(scatterShot);
        ScatterShotScript firedBullet = createBullet.GetComponent<ScatterShotScript>();
        firedBullet.GetValues(this.transform, shotSpeed, direction);
    }
}
