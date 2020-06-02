using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.AI;

public class AiProcessing : MonoBehaviour
{
    //Jeff Code
    protected bool inCombat, isRetreating, targetSpotted, isArmed;

    [Header("Mark as Civilian to disable combat")]
    [SerializeField] protected bool isCivilian;

    [Header("Scripts Only Add on AiProcessing")]
    private AiCombat aicombat;
    private AiMovement aiMovement;
    [SerializeField] protected FieldOfView fieldOfViewSight;
    [SerializeField] protected FieldOfView fieldOfViewHearing;

    [Header("Add Weapon to enable combat")]
    [SerializeField] private GameObject weaponGameObject;

    [Header("Set Current Health, Only on Processor")]
    [SerializeField] public float currentHealth;

    [Header("Set Death Animation, Only on Processor")]
    [SerializeField] private GameObject DeathAnimation;
   
    [Header("Set score value, Only on Processor")]
    [SerializeField] private int scoreAmount ;
    [Header("Set HPBar from HUD")]
    [SerializeField] public BossHPbarScript healthBar;

    protected Vector3 direction;
    protected Vector3 rotation;
    protected float RotationSpeed = 10f;
    protected float patrolSpeed;

    protected Transform target;

    protected float distance;
    protected Transform destination;


    void Start()
    {
        aicombat = GetComponentInChildren<AiCombat>();
        aiMovement = GetComponentInChildren<AiMovement>();
     

        InvokeRepeating("GetNearestTarget", 0f, 0.1f);
        InvokeRepeating("LookAround", 0f, 0.5f);
        CheckIfIsArmed();
        if (aicombat != null)
        {
            aicombat.isCivilian = isCivilian;
        }
        if (aiMovement != null)
        {
            aiMovement.isCivilian = isCivilian;
        }
        inCombat = false;
        if (aicombat != null)
        {
            aicombat.inCombat = false;
        }
        if (aiMovement != null)
        {
            aiMovement.inCombat = false;
        }
        if (aicombat != null)
        {
            aicombat.currentHealth = currentHealth;
        }
        aiMovement.currentHealth = currentHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxhealth(currentHealth);
        }
    }

    void Update()
    {

    }

    public void takeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
        //aicombat.currentHealth -= damageTaken;
        //aiMovement.currentHealth -= damageTaken;


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

    //Checks if Ai Is armed 
    void CheckIfIsArmed()
    {
        if (weaponGameObject == null)
        {
            isArmed = false;
            if (aicombat != null)
            {
                aicombat.isArmed = false;
            }
        }
        else
        {
            isArmed = true;
            if (aicombat != null)
            {
                aicombat.isArmed = true;
            }
        }
    }
    void LookAround()
    {
        if (target == null)
        {
            if (fieldOfViewSight.visableTargets.Count <= 0)
            {
                aiMovement.target = null;
                target = null;
            }
            if (fieldOfViewHearing.visableTargets.Count <= 0)
            {
                aiMovement.target = null;
                target = null;
            }

            inCombat = false;
            if (aiMovement != null)
            {
                aiMovement.inCombat = false;
            }
            if (aicombat != false)
            {
                aicombat.inCombat = false;
            }
        }
        if (target != null)
        {
            if (aiMovement != null)
            {
                aiMovement.inCombat = true;
            }
            if (aicombat != null)
            {
                aicombat.inCombat = true;
            }
            inCombat = true;
            GetNearestTarget();
        }
    }

    void GetNearestTarget()
    {
        aiMovement.target = null;
        target = null;
        if (fieldOfViewSight.visableTargets.Count > 0)
        {
            target = fieldOfViewSight.visableTargets[0];
            aiMovement.target = target;
        }
        if (fieldOfViewHearing.visableTargets.Count > 0)
        {
            target = fieldOfViewHearing.visableTargets[0];
            aiMovement.target = target;

        }
    }
    protected void FleeBehavior()
    {
        isRetreating = true;
        if (aiMovement != null)
        {
            aiMovement.isRetreating = true;
        }
        if (aicombat != null)
        {
            aicombat.isRetreating = true;
        }
    }
}
