using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject[] allWeapons;
    [SerializeField] GunShotScript weapon;
    GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
    GameObject target;
    Pickup pickUp;
    float pickupRadius = 5;
    Vector3 direction;
    [SerializeField] float fireCd = 4f;
    [SerializeField] float timeSinceLastShot = 0;


    // Start is called before the first frame update
    void Start()
    {
        fireCd = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            allWeapons = GameObject.FindGameObjectsWithTag("Gun");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //weapon = GameObject.Find("Pistol").GetComponent<GunShotScript>();
            weapon = allWeapons[0].GetComponent<GunShotScript>();
        }
        target = GameObject.FindGameObjectWithTag("Target");

        direction = target.transform.position - this.transform.position;

        //Försöker avfyra en kula
        if (Input.GetMouseButton(0) == true && timeSinceLastShot > fireCd)
        {
            weapon.Shoot();
            timeSinceLastShot = 0;
        }

    }
}