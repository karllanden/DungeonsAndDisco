using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIMasterScript : MonoBehaviour
{
    //Jeff
    [SerializeField]
    public float maxHp;
    [SerializeField]
    private float currentHp;

    NavMeshAgent agent;
    private Transform destination;

    public Transform[] patrolPathsArray;
    public Transform NearestPatrolPoint;
    private int NearestPartolNumber;

    public Transform target;
    public Transform baseRotation;

    public FieldOfView fieldOfViewSight;
    public FieldOfView fieldOfViewHearing;

    public string TargetMarkerTag = "DestroyableEnviormet";
    private float agentSpeed;
    private Vector3 rotation;
    public float RotationSpeed = 10f;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agentSpeed = agent.speed / 2; 
        InvokeRepeating("GetNearestTarget", 0f, 0.1f);
        InvokeRepeating("SeekPatrolPath", 0f, 4.0f);
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
        if (target == null)
        {
            if (fieldOfViewSight.visableTargets.Count <= 0)
            {
                target = null;
            }
            if (fieldOfViewHearing.visableTargets.Count <= 0)
            {
                target = null;
            }
            agent.speed = agentSpeed;
            return;
        }

        Vector3 direction = target.position - transform.position;
        Quaternion FindTargetRotation = Quaternion.LookRotation(direction);
        rotation = Quaternion.Lerp(baseRotation.rotation, FindTargetRotation, Time.deltaTime * RotationSpeed).eulerAngles;
        baseRotation.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }
    void GetNearestTarget()
    {
        target = null;
        if (fieldOfViewSight.visableTargets.Count > 0)
        {
            target = fieldOfViewSight .visableTargets[0];
        }
        if (fieldOfViewHearing.visableTargets.Count > 0)
        {
            target = fieldOfViewHearing.visableTargets[0];
        }
    }
    void SeekPatrolPath()
    {
        if (target == null)
        {
            GetPartolPath();
        }
    }
    void GetPartolPath()
    {
        //NearestPartolNumber = patrolPathsArray.Length;
        NearestPartolNumber = Random.Range(0, patrolPathsArray.Length);
        NearestPatrolPoint = patrolPathsArray[NearestPartolNumber];
        agent.SetDestination(NearestPatrolPoint.position);
        //if (agent.destination == NearestPatrolPoint.position)
        //{

        //}
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfViewSight.viewRadius);
    }
}
