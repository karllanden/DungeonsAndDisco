using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiProcessing : MonoBehaviour
{
    protected bool inCombat, isRetreating, targetSpotted, isArmed;
    [SerializeField]
    protected bool isCivilian;
    //protected static NavMeshAgent agent;
    protected float patrolSpeed;

    [SerializeField]
    private AiCombat aicombat;
    [SerializeField]
    private AiMovement aiMovement;

    [SerializeField]
    protected FieldOfView fieldOfViewSight;
    [SerializeField]
    protected FieldOfView fieldOfViewHearing;
    public Transform target;

    protected float distance;
    protected Transform destination;
    [SerializeField]
    private GameObject weaponGameObject;

    protected Vector3 direction;
    protected Vector3 rotation;
    protected float RotationSpeed = 10f;


    [SerializeField]
    public float currentHealth;

    [SerializeField]
    public GameObject DeathAnimation;



    // Start is called before the first frame update
    void Start()
    {
        //agent = this.GetComponent<NavMeshAgent>();
        //patrolSpeed = agent.speed / 2;
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

        currentHealth = 5f;
    }

    // Update is called once per frame
    void Update()
    {

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
            //agent.speed = patrolSpeed;
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
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, fieldOfViewSight.viewRadius);
    //}
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
