using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject[] allWeapons;
    [SerializeField] GameObject weapon;
    GameObject[] allEnemies;
    GameObject target;
    Pickup pickUp;
    float pickupRadius = 10;
    Vector3 direction;
    [SerializeField] float fireCd = 4f;
    [SerializeField] float timeSinceLastShot = 0;


    // Start is called before the first frame update
    void Start()
    {
        fireCd = 0.2f;
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
            IdentifyWeaponOnPickup();


        target = GameObject.FindGameObjectWithTag("Target");

        direction = target.transform.position - this.transform.position;

        //Försöker avfyra en kula
        if (Input.GetMouseButton(0) == true && timeSinceLastShot > fireCd)
        {
            if (weapon.GetComponent<GunShotScript>())
            {
                weapon.GetComponent<GunShotScript>().Shoot();
            }
            else if (weapon.GetComponent<ShotGunScript>())
            {
                weapon.GetComponent<ShotGunScript>().Shoot();
            }
            else if (weapon.GetComponent<BurstFireScript>())
            {
                weapon.GetComponent<BurstFireScript>().Shoot();
            }

            timeSinceLastShot = 0;
        }

    }

    //Letar upp vilket objekt som placeras i handen
    private void IdentifyWeaponOnPickup()
    {


        allWeapons = GameObject.FindGameObjectsWithTag("Gun");
        float distanceToCurrentClosestWeapon = Mathf.Infinity;
        GameObject tempWeaponSelected = new GameObject();
        foreach (GameObject g in allWeapons)
        {
            Vector3 distanceToNextWeapon = g.transform.position - gameObject.transform.position;
            if (distanceToCurrentClosestWeapon > distanceToNextWeapon.magnitude)
            {
                distanceToCurrentClosestWeapon = distanceToNextWeapon.magnitude;
                if (distanceToCurrentClosestWeapon < pickupRadius)
                {
                    tempWeaponSelected = g;
                }

            }
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //weapon = GameObject.Find("Pistol").GetComponent<GunShotScript>();
        if (tempWeaponSelected != null)
        {
            weapon = tempWeaponSelected;
        }


    }
}
