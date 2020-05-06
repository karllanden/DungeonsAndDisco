using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiCombat : AiProcessing
{
    [SerializeField]
    GunShotScript weapon;
    [SerializeField] float damage;
    [SerializeField] AudioManager audioManager;

    [SerializeField] public float currentHealth;

    [SerializeField]
    public GameObject DeathAnimation;

    float fireCd = 0.2f, timeSinceLastShot = 0, shotSpeed = 50;
    
    void Start()
    {
        currentHealth = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (inCombat == true)
        {
            CombatBehaivor();
        }
        if (isCivilian == true || isRetreating == true)
        {
            Flee();
        }

    }
    void Flee()
    {
        FleeBehavior();
    }
    void CombatBehaivor()
    {
        timeSinceLastShot += Time.deltaTime;
        if (/*fieldOfViewSight.directLineOfSight == true && */timeSinceLastShot > fireCd)
        {
            weapon.Shoot();
            audioManager.Play("PistolShot");
            timeSinceLastShot = 0;
        }
    }
    public void takeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;



        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Vector3 pos = this.transform.position;
        DeathAnimation = GameObject.Instantiate(DeathAnimation, pos, Quaternion.identity);
        this.gameObject.SetActive(false);
    }
}
