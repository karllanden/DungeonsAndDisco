using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiCombat : AiProcessing
{
    [SerializeField]
    GunShotScript weapon;
    [SerializeField] float damage;

    float fireCd = 0.2f, timeSinceLastShot = 0, shotSpeed = 50;
    
    void Start()
    {
        
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
            timeSinceLastShot = 0;
        }
    }
}
