using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiCombat : AiProcessing
{
    //Jeff Code
    [Header("Set Weapon Damage and add weapon script")]
    [SerializeField] GunShotScript weapon;
    [SerializeField] float damage;


    float fireCd = 0.2f, timeSinceLastShot = 0, shotSpeed = 50;
    
    void Start()
    {

    }

    void Update()
    {
        if (inCombat == true)
        {
            CombatBehaivor();
        }
        if (isCivilian == true || isRetreating == true)
        {
            FleeBehavior();
        }

    }
    void CombatBehaivor()
    {
        if (isArmed == false)
        {
            return;
        }
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot > fireCd)
        {
            weapon.Shoot();
            timeSinceLastShot = 0;
        }
    }


}
