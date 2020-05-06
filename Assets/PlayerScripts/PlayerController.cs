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
    float fireCdQ;
    float fireCdE;
    [SerializeField] float timeSinceLastShotQ = 0;
    [SerializeField] float timeSinceLastShotE = 0;
    [SerializeField] float reloadTimeQ;
    float reloadTimeCountQ;
    [SerializeField] float reloadTimeE;
    float reloadTimeCountE;

    [SerializeField]
    Text currentAmmoQ;
    [SerializeField]
    Text currentAmmoE;

    [SerializeField]
    AudioManager audioManager;

    [SerializeField] public Image PistolIconQ, PistolIconE, ShotgunIconQ, ShotgunIconE, BurstIconQ, BurstIconE, AKIconQ, AKIconE;




    // Start is called before the first frame update
    void Start()
    {
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        PistolIconQ.enabled = false;
        PistolIconE.enabled = false;
        ShotgunIconQ.enabled = false;
        ShotgunIconE.enabled = false;
        BurstIconQ.enabled = false;
        BurstIconE.enabled = false;
        AKIconQ.enabled = false;
        AKIconE.enabled = false;
    }

    // Update is called once per frame
    // Hampus and Karl
    void Update()
    {
        timeSinceLastShotQ += Time.deltaTime;
        timeSinceLastShotE += Time.deltaTime;
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
            IdentifyWeaponOnPickup();


        target = GameObject.FindGameObjectWithTag("Target");

        direction = target.transform.position - this.transform.position;

        
        if (weapon != null) //chech if player has weapon equipped.
        {
            UpdateHUD();
            UpdateHUDimage();
            ReloadQ(); //try reload Q
            ReloadE(); //try reload E

        }

        //Försöker avfyra en kula
        if (Input.GetMouseButton(0) == true && timeSinceLastShotQ > fireCdQ)
        {
            ShootQ();
        }
        if (Input.GetMouseButton(1) == true && timeSinceLastShotE > fireCdE)
        {
            ShootE();
        }


    }

    private void UpdateHUD()
    {
        if (handE.transform.childCount > 0)
        {
            if (handE.GetComponentInChildren<GunShotScript>()) //access gunshotscript of weapon in hand E.
            {
                currentAmmoE.text = handE.GetComponentInChildren<GunShotScript>().currentAmmo.ToString(); //update ammoHUD for weapon in hand E.
            }
            if (handE.GetComponentInChildren<ShotGunScript>()) //access gunshotscript of weapon in hand E.
            {
                currentAmmoE.text = handE.GetComponentInChildren<ShotGunScript>().currentAmmo.ToString(); //update ammoHUD for weapon in hand E.
            }
            if (handE.GetComponentInChildren<BurstFireScript>()) //access gunshotscript of weapon in hand E.
            {
                currentAmmoE.text = handE.GetComponentInChildren<BurstFireScript>().currentAmmo.ToString(); //update ammoHUD for weapon in hand E.
            }
        }


        if (handE.transform.childCount > 0)
        {
            if (handQ.GetComponentInChildren<GunShotScript>()) //access gunshotscript of weapon in hand Q.
            {
                currentAmmoQ.text = handQ.GetComponentInChildren<GunShotScript>().currentAmmo.ToString(); //update ammoHUD for weapon in hand Q.
            }
            if (handQ.GetComponentInChildren<ShotGunScript>()) //access gunshotscript of weapon in hand Q.
            {
                currentAmmoQ.text = handQ.GetComponentInChildren<ShotGunScript>().currentAmmo.ToString(); //update ammoHUD for weapon in hand Q.
            }
            if (handQ.GetComponentInChildren<BurstFireScript>()) //access gunshotscript of weapon in hand Q.
            {
                currentAmmoQ.text = handQ.GetComponentInChildren<BurstFireScript>().currentAmmo.ToString(); //update ammoHUD for weapon in hand Q.
            }
        }

        if (handE.transform.childCount == 0)
        {
            currentAmmoE.text = "0";
        }
        if (handQ.transform.childCount == 0)
        {
            currentAmmoQ.text = "0";
        }


        if (handQ.GetComponentInChildren<ParticleSystem>()) // does weapon have static particle effect?
        {
            handQ.GetComponentInChildren<ParticleSystem>().Pause(); //pause at pickup
            handQ.GetComponentInChildren<ParticleSystem>().Clear(); //clear at pickup
            if (handQ.GetComponentInChildren<Text>()) //does weapon have text-UI-object-thingy?
            {
                handQ.GetComponentInChildren<Text>().enabled = false; //remove info-text from weapon at pickup.
            }
        }
        if (handE.GetComponentInChildren<ParticleSystem>()) // does weapon have static particle effect?
        {
            handE.GetComponentInChildren<ParticleSystem>().Pause(); //pause at pickup
            handE.GetComponentInChildren<ParticleSystem>().Clear(); //clear at pickup
            if (handE.GetComponentInChildren<Text>()) //does weapon have text-UI-object-thingy?
            {
                handE.GetComponentInChildren<Text>().enabled = false; //remove info-text from weapon at pickup.
            }
        }
    }
    private void UpdateHUDimage()
    {
        if (handQ.GetComponentInChildren<GunShotScript>())
        {
            PistolIconQ.enabled = true;
            ShotgunIconQ.enabled = false;
            BurstIconQ.enabled = false;
            AKIconQ.enabled = false;
        }
        if (handQ.GetComponentInChildren<ShotGunScript>())
        {
            PistolIconQ.enabled = false;
            ShotgunIconQ.enabled = true;
            BurstIconQ.enabled = false;
            AKIconQ.enabled = false;
        }
        if (handQ.GetComponentInChildren<BurstFireScript>())
        {
            PistolIconQ.enabled = false;
            ShotgunIconQ.enabled = false;
            BurstIconQ.enabled = true;
            AKIconQ.enabled = false;
        }

        if (handE.GetComponentInChildren<GunShotScript>())
        {
            PistolIconE.enabled = true;
            ShotgunIconE.enabled = false;
            BurstIconE.enabled = false;
            AKIconE.enabled = false;
        }
        if (handE.GetComponentInChildren<ShotGunScript>())
        {
            PistolIconE.enabled = false;
            ShotgunIconE.enabled = true;
            BurstIconE.enabled = false;
            AKIconE.enabled = false;
        }
        if (handE.GetComponentInChildren<BurstFireScript>())
        {
            PistolIconE.enabled = false;
            ShotgunIconE.enabled = false;
            BurstIconE.enabled = true;
            AKIconE.enabled = false;
        }
    }
    private void UpdateWeaponStatsQ()
    {
        if (handQ.GetComponentInChildren<GunShotScript>())
            handQ.GetComponentInChildren<GunShotScript>().currentAmmo--;

        if (handQ.GetComponentInChildren<ShotGunScript>())
            handQ.GetComponentInChildren<ShotGunScript>().currentAmmo--;

        if (handQ.GetComponentInChildren<BurstFireScript>())
            handQ.GetComponentInChildren<BurstFireScript>().currentAmmo -= 3;

    }
    private void UpdateWeaponStatsE()
    {
        if (handE.GetComponentInChildren<GunShotScript>())
            handE.GetComponentInChildren<GunShotScript>().currentAmmo--;

        if (handE.GetComponentInChildren<ShotGunScript>())
            handE.GetComponentInChildren<ShotGunScript>().currentAmmo--;

        if (handE.GetComponentInChildren<BurstFireScript>())
            handE.GetComponentInChildren<BurstFireScript>().currentAmmo -= 3;
    }

    private void ShootQ()
    {
        if (handQ.GetComponentInChildren<GunShotScript>() || handQ.GetComponentInChildren<ShotGunScript>() || handQ.GetComponentInChildren<BurstFireScript>())
        {

            if (handQ.GetComponentInChildren<GunShotScript>())
            {
                if (handQ.GetComponentInChildren<GunShotScript>().currentAmmo >= 1)
                {
                    fireCdQ = 0.2f;
                    handQ.GetComponentInChildren<GunShotScript>().Shoot();
                    audioManager.Play("PistolShot");
                    UpdateWeaponStatsQ();
                    timeSinceLastShotQ = 0;
                }

            }
            if (handQ.GetComponentInChildren<ShotGunScript>())
            {
                if (handQ.GetComponentInChildren<ShotGunScript>().currentAmmo >= 1)
                {
                    fireCdQ = 0.5f;
                    
                    
                    handQ.GetComponentInChildren<ShotGunScript>().Shoot();
                    audioManager.Play("PistolShot");
                    UpdateWeaponStatsQ();
                    timeSinceLastShotQ = 0;
                }
            }
            if (handQ.GetComponentInChildren<BurstFireScript>())
            {
                if (handQ.GetComponentInChildren<BurstFireScript>().currentAmmo >= 3)
                {
                    fireCdQ = 0.3f;
                    handQ.GetComponentInChildren<BurstFireScript>().Shoot();
                    audioManager.Play("PistolShot");
                    UpdateWeaponStatsQ();
                    timeSinceLastShotQ = 0;
                }
            }
        }
    }
    private void ShootE()
    {
        if (handE.GetComponentInChildren<GunShotScript>() || handE.GetComponentInChildren<ShotGunScript>() || handE.GetComponentInChildren<BurstFireScript>())
        {

            if (handE.GetComponentInChildren<GunShotScript>())
            {
                if (handE.GetComponentInChildren<GunShotScript>().currentAmmo >= 1)
                {
                    fireCdE = 0.2f;
                    handE.GetComponentInChildren<GunShotScript>().Shoot();
                    audioManager.Play("PistolShot");
                    UpdateWeaponStatsE();
                    timeSinceLastShotE = 0;
                }

            }
            if (handE.GetComponentInChildren<ShotGunScript>())
            {
                if (handE.GetComponentInChildren<ShotGunScript>().currentAmmo >= 1)
                {
                    fireCdE = 0.5f;
                    handE.GetComponentInChildren<ShotGunScript>().Shoot();
                    audioManager.Play("PistolShot");
                    UpdateWeaponStatsE();
                    timeSinceLastShotE = 0;
                }
            }
            if (handE.GetComponentInChildren<BurstFireScript>())
            {
                if (handE.GetComponentInChildren<BurstFireScript>().currentAmmo >= 3)
                {
                    fireCdE = 0.3f;
                    handE.GetComponentInChildren<BurstFireScript>().Shoot();
                    audioManager.Play("PistolShot");
                    UpdateWeaponStatsE();
                    timeSinceLastShotE = 0;
                }
            }
        }
        
    }

    private void ReloadQ()
    {
        if (handQ.GetComponentInChildren<GunShotScript>())
        {
            if (handQ.GetComponentInChildren<GunShotScript>().currentAmmo <= 0) //start timer when ammo is 0.
            {
                reloadTimeCountQ += Time.deltaTime;
            }
            if (reloadTimeCountQ > reloadTimeQ) //reset (reload)
            {
                handQ.GetComponentInChildren<GunShotScript>().currentAmmo = handQ.GetComponentInChildren<GunShotScript>().maxAmmo;
                reloadTimeCountQ = 0;
            }
        }
        if (handQ.GetComponentInChildren<ShotGunScript>())
        {
            if (handQ.GetComponentInChildren<ShotGunScript>().currentAmmo <= 0) //start timer when ammo is 0.
            {
                reloadTimeCountQ += Time.deltaTime;
            }
            if (reloadTimeCountQ > reloadTimeQ) //reset (reload)
            {
                handQ.GetComponentInChildren<ShotGunScript>().currentAmmo = handQ.GetComponentInChildren<ShotGunScript>().maxAmmo;
                reloadTimeCountQ = 0;
            }
        }
        if (handQ.GetComponentInChildren<BurstFireScript>())
        {
            if (handQ.GetComponentInChildren<BurstFireScript>().currentAmmo <= 3) //start timer when ammo is 0.
            {
                reloadTimeCountQ += Time.deltaTime;
            }
            if (reloadTimeCountQ > reloadTimeQ) //reset (reload)
            {
                handQ.GetComponentInChildren<BurstFireScript>().currentAmmo = handQ.GetComponentInChildren<BurstFireScript>().maxAmmo;
                reloadTimeCountQ = 0;
            }
        }
    }
    private void ReloadE()
    {
        if (handE.GetComponentInChildren<GunShotScript>())
        {
            if (handE.GetComponentInChildren<GunShotScript>().currentAmmo <= 0) //start timer when ammo is 0.
            {
                reloadTimeCountE += Time.deltaTime;
            }
            if (reloadTimeCountE > reloadTimeE) //reset (reload)
            {
                handE.GetComponentInChildren<GunShotScript>().currentAmmo = handE.GetComponentInChildren<GunShotScript>().maxAmmo;
                reloadTimeCountQ = 0;
            }
        }
        if (handE.GetComponentInChildren<ShotGunScript>())
        {
            if (handE.GetComponentInChildren<ShotGunScript>().currentAmmo <= 0) //start timer when ammo is 0.
            {
                reloadTimeCountE += Time.deltaTime;
            }
            if (reloadTimeCountE > reloadTimeE) //reset (reload)
            {
                handE.GetComponentInChildren<ShotGunScript>().currentAmmo = handE.GetComponentInChildren<ShotGunScript>().maxAmmo;
                reloadTimeCountE = 0;
            }
        }
        if (handE.GetComponentInChildren<BurstFireScript>())
        {
            if (handE.GetComponentInChildren<BurstFireScript>().currentAmmo <= 3) //start timer when ammo is 0.
            {
                reloadTimeCountE += Time.deltaTime;
            }
            if (reloadTimeCountE > reloadTimeE) //reset (reload)
            {
                handE.GetComponentInChildren<BurstFireScript>().currentAmmo = handE.GetComponentInChildren<BurstFireScript>().maxAmmo;
                reloadTimeCountE = 0;
            }
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
