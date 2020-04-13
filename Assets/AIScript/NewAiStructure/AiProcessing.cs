using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiProcessing : MonoBehaviour
{
    protected bool inCombat, isRetreating, targetSpotted, isArmed;
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

    public GameObject weaponGameObject;

    protected Vector3 direction;
    protected Vector3 rotation;
    protected float RotationSpeed = 10f;

    protected Transform fleePoint;


    // Start is called before the first frame update
    void Start()
    {
        //agent = this.GetComponent<NavMeshAgent>();
        //patrolSpeed = agent.speed / 2;
        InvokeRepeating("GetNearestTarget", 0f, 0.1f);
        InvokeRepeating("LookAround", 0f, 0.5f);
        CheckIfIsArmed();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void CheckIfIsArmed()
    {
        if (weaponGameObject != null)
        {
            isArmed = false;
        }
        else
        {
            isArmed = true;
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
            aiMovement.inCombat = false;
            aicombat.inCombat = false;
        }
        if (target != null)
        {
            aiMovement.inCombat = true;
            aicombat.inCombat = true;
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
        //agent.SetDestination(fleePoint.position);
    }
}
