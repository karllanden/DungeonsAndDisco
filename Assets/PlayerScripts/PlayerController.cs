using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Hampus & Karl

    [SerializeField]
    GameObject[] allWeapons;
    [SerializeField]
    GameObject weapon;
    [SerializeField]
    GameObject handE;
    [SerializeField]
    GameObject handQ;
    GameObject[] allEnemies;
    GameObject target;
    Pickup pickUp;
    float pickupRadius = 10;
    Vector3 direction;
    float fireCdQ;
    float fireCdE;
    [SerializeField]
    float timeSinceLastShotQ = 0;
    [SerializeField]
    float timeSinceLastShotE = 0;
    [SerializeField]
    float reloadTimeQ;
    float reloadTimeCountQ;
    [SerializeField]
    float reloadTimeE;
    float reloadTimeCountE;

    [SerializeField]
    Text currentAmmoQ;
    [SerializeField]
    Text currentAmmoE;

    [SerializeField]
    public Image PistolIconQ, PistolIconE, ShotgunIconQ, ShotgunIconE, BurstIconQ, BurstIconE, AKIconQ, AKIconE; // all the icons for the HUD

    public static bool gameIsPaused = false;
    [SerializeField]
    GameObject PauseMenu;

    // Start is called before th    {
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameIsPaused)
            {
                PauseMenu.SetActive(true);
                gameIsPaused = true;
                Time.timeScale = 0;

            }
            else
            {
                PauseMenu.SetActive(false);
                gameIsPaused = false;
                Time.timeScale = 1;

            }
        }
        if (!gameIsPaused)
        {
            timeSinceLastShotQ += Time.deltaTime;
            timeSinceLastShotE += Time.deltaTime;
            if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
                IdentifyWeaponOnPickup();


            target = GameObject.FindGameObjectWithTag("Target");

            direction = target.transform.position - this.transform.position;

            float distancePlayerTarget = Vector3.Distance(this.transform.position, target.transform.position);

            if (distancePlayerTarget < 3)
            {
            }
            else
            {
                handE.transform.LookAt(target.transform);
                handQ.transform.LookAt(target.transform);
            }

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


        if (handQ.transform.childCount > 0)
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
            if (handQ.GetComponentInChildren<GunShotScript>().fireCd < 0.1f)
            {
                AKIconQ.enabled = true;
                PistolIconQ.enabled = false;
                ShotgunIconQ.enabled = false;
                BurstIconQ.enabled = false;
            }
            else
            {
                PistolIconQ.enabled = true;
                AKIconQ.enabled = false;
                ShotgunIconQ.enabled = false;
                BurstIconQ.enabled = false;
            }


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
            if (handE.GetComponentInChildren<GunShotScript>().fireCd < 0.1f)
            {
                AKIconE.enabled = true;
                PistolIconE.enabled = false;
                ShotgunIconE.enabled = false;
                BurstIconE.enabled = false;
            }
            else
            {
                AKIconE.enabled = false;
                PistolIconE.enabled = true;
                ShotgunIconE.enabled = false;
                BurstIconE.enabled = false;
            }
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

        if (handQ.transform.childCount <= 0)
        {
            PistolIconQ.enabled = false;
            ShotgunIconQ.enabled = false;
            BurstIconQ.enabled = false;
            AKIconQ.enabled = false;
        }
        if (handE.transform.childCount <= 0)
        {
            PistolIconE.enabled = false;
            ShotgunIconE.enabled = false;
            BurstIconE.enabled = false;
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
                    fireCdQ = handQ.GetComponentInChildren<GunShotScript>().fireCd;
                    handQ.GetComponentInChildren<GunShotScript>().Shoot();
                    UpdateWeaponStatsQ();
                    timeSinceLastShotQ = 0;
                }
                else
                {
                    fireCdQ = 0.2f;
                    FindObjectOfType<AudioManager>().Play("EmptyClip");
                    timeSinceLastShotQ = 0;
                }

            }
            if (handQ.GetComponentInChildren<ShotGunScript>())
            {
                if (handQ.GetComponentInChildren<ShotGunScript>().currentAmmo >= 1)
                {
                    fireCdQ = handQ.GetComponentInChildren<ShotGunScript>().fireCd;
                    handQ.GetComponentInChildren<ShotGunScript>().Shoot();
                    UpdateWeaponStatsQ();
                    timeSinceLastShotQ = 0;
                }
                else
                {
                    fireCdQ = 0.5f;
                    FindObjectOfType<AudioManager>().Play("EmptyClip");
                    timeSinceLastShotQ = 0;
                }
            }
            if (handQ.GetComponentInChildren<BurstFireScript>())
            {
                if (handQ.GetComponentInChildren<BurstFireScript>().currentAmmo > 3)
                {
                    fireCdQ = 0.3f;
                    handQ.GetComponentInChildren<BurstFireScript>().Shoot();
                    UpdateWeaponStatsQ();
                    timeSinceLastShotQ = 0;
                }
                else
                {
                    fireCdQ = 0.3f;
                    FindObjectOfType<AudioManager>().Play("EmptyClip");
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
                    fireCdE = handE.GetComponentInChildren<GunShotScript>().fireCd;
                    handE.GetComponentInChildren<GunShotScript>().Shoot();
                    UpdateWeaponStatsE();
                    timeSinceLastShotE = 0;
                }
                else
                {
                    fireCdE = 0.2f;
                    FindObjectOfType<AudioManager>().Play("EmptyClip");
                    timeSinceLastShotE = 0;
                }

            }
            if (handE.GetComponentInChildren<ShotGunScript>())
            {
                if (handE.GetComponentInChildren<ShotGunScript>().currentAmmo >= 1)
                {
                    fireCdE = handE.GetComponentInChildren<ShotGunScript>().fireCd;
                    handE.GetComponentInChildren<ShotGunScript>().Shoot();
                    UpdateWeaponStatsE();
                    timeSinceLastShotE = 0;
                }
                else
                {
                    fireCdE = 0.5f;
                    FindObjectOfType<AudioManager>().Play("EmptyClip");
                    timeSinceLastShotE = 0;
                }
            }
            if (handE.GetComponentInChildren<BurstFireScript>())
            {
                if (handE.GetComponentInChildren<BurstFireScript>().currentAmmo > 3)
                {
                    fireCdE = 0.3f;
                    handE.GetComponentInChildren<BurstFireScript>().Shoot();
                    UpdateWeaponStatsE();
                    timeSinceLastShotE = 0;
                }
                else
                {
                    fireCdE = 0.3f;
                    FindObjectOfType<AudioManager>().Play("EmptyClip");
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
            if (handQ.GetComponentInChildren<BurstFireScript>().currentAmmo <= 3) //start timer when ammo is 3 or below.
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
                reloadTimeCountE = 0;
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