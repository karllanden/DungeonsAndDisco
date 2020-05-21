using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;

public class GunShotScript : MonoBehaviour
{
    public AudioSource audio;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawn;
    public int maxAmmo, currentAmmo;
    float fireCd = 0.2f, timeSinceLastShot = 0;
    [SerializeField]
    float shotSpeed = 50;
    Vector3 direction;
    GameObject target, hand;
    [SerializeField] public float damage;
    public bool isPlayer;
    public List<AudioSource> audioSources;
    //Hittar kulornas mål och beräknar riktning

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        direction = transform.forward;
        target = GameObject.FindGameObjectWithTag("Target");
        // direction = target.transform.position - this.transform.position;
        timeSinceLastShot += Time.deltaTime;
        ////Försöker avfyra en kula
        //if (Input.GetMouseButton(0) == true && timeSinceLastShot > fireCd)
        //{
        //    Shoot();
        //    timeSinceLastShot = 0;
        //    
        //}
    }
    //Om en kula kan avfyras skapas den och ges värden
    public void Shoot()
    {
        GameObject createBullet = GameObject.Instantiate(bullet);
        AudioSource newAudio = audio;
        newAudio.pitch = audio.pitch;
        newAudio.volume = audio.volume;
        newAudio.PlayOneShot(audio.clip);
        audioSources.Add(newAudio);

        Bullet firedBullet = createBullet.GetComponent<Bullet>();
        firedBullet.GetValues(bulletSpawn.transform, shotSpeed, direction, damage);

        //FindObjectOfType<AudioManager>().Play("PistolShot");
    }
}