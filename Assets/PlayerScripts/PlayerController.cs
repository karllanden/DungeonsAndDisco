using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject[] allWeapons;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject handE;
    [SerializeField] GameObject handQ;
    GameObject[] allEnemies;
    GameObject target;
    Pickup pickUp;
    float pickupRadius = 10;
    Vector3 direction;
    [SerializeField] float fireCd;
    [SerializeField] float timeSinceLastShot = 0;
    [SerializeField] float reloadTime;
    float reloadTimeCount;

    [SerializeField]
    Text currentAmmoQ;
    [SerializeField]
    Text currentAmmoE;

    [SerializeField]
    AudioManager audioManager;




    // Start is called before the first frame update
    void Start()
    {
        fireCd = 0.2f;
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        
    }

    // Update is called once per frame
    // Hampus and Karl
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
            IdentifyWeaponOnPickup();


        target = GameObject.FindGameObjectWithTag("Target");

        direction = target.transform.position - this.transform.position;

        
        if (weapon != null) //chech if player has weapon equipped.
        {
            if (handE.GetComponentInChildren<GunShotScript>()) //access gunshotscript of weapon in hand E.
            {
                currentAmmoE.text = handE.GetComponentInChildren<GunShotScript>().currentAmmo.ToString(); //update ammoHUD for weapon in hand E.
            }
            if (handQ.GetComponentInChildren<GunShotScript>()) //access gunshotscript of weapon in hand Q.
            {
                currentAmmoQ.text = handQ.GetComponentInChildren<GunShotScript>().currentAmmo.ToString(); //update ammoHUD for weapon in hand Q.
            }
            if (weapon.GetComponentInChildren<ParticleSystem>()) // does weapon have static particle effect?
            {
                weapon.GetComponentInChildren<ParticleSystem>().Pause(); //pause at pickup
                weapon.GetComponentInChildren<ParticleSystem>().Clear(); //clear at pickup

                weapon.GetComponentInChildren<Text>().enabled = false; //remove info-text from weapon at pickup.
            }
            //handE.GetComponentInChildren<GunShotScript>().currentAmmo

            if (weapon.GetComponent<GunShotScript>().currentAmmo <= 0) //start timer when ammo is 0.
            {
                reloadTimeCount += Time.deltaTime;
            }
            if (reloadTimeCount > reloadTime) //reset (reload)
            {
                weapon.GetComponent<GunShotScript>().currentAmmo = weapon.GetComponent<GunShotScript>().maxAmmo;
                reloadTimeCount = 0;
            }
        }

        //Försöker avfyra en kula
        if (Input.GetMouseButton(0) == true && timeSinceLastShot > fireCd)
        {
            if (weapon != null)
            {
                if (weapon.GetComponent<GunShotScript>().currentAmmo >= 1)
                {
                    if (weapon.GetComponent<GunShotScript>())
                    {
                        weapon.GetComponent<GunShotScript>().Shoot();
                        audioManager.Play("PistolShot");
                        UpdateWeaponStats();

                    }
                    else if (weapon.GetComponent<ShotGunScript>())
                    {
                        weapon.GetComponent<ShotGunScript>().Shoot();
                        audioManager.Play("PistolShot");
                        UpdateWeaponStats();
                    }
                    if (weapon.GetComponent<GunShotScript>().currentAmmo >= 3)
                    {
                        if (weapon.GetComponent<BurstFireScript>())
                        {
                            weapon.GetComponent<BurstFireScript>().Shoot();
                            audioManager.Play("PistolShot");
                            UpdateWeaponStats();
                        }
                    }
                }
            }

            timeSinceLastShot = 0;
        }

    }

    private void UpdateWeaponStats()
    {
        weapon.GetComponent<GunShotScript>().currentAmmo--;
        if (weapon.GetComponent<BurstFireScript>() && weapon.GetComponent<GunShotScript>().currentAmmo >=3)
        {
            weapon.GetComponent<GunShotScript>().currentAmmo -= 2;
        }
        //if (weapon.GetComponent<GunShotScript>().currentAmmo <= 0)
        //{
        //    Reload();
        //}
        
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
