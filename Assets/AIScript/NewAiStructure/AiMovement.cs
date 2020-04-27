using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : AiProcessing
{
    public Transform[] patrolPathsArray;
    public Transform nearestPatrolPoint;
    private int nearestPartolNumber;
    public bool atDestination;
    public Transform baseRotation;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        //nearestPartolNumber = Random.Range(0, patrolPathsArray.Length);
        //nearestPatrolPoint = patrolPathsArray[nearestPartolNumber];
        //agent.SetDestination(nearestPatrolPoint.position);
        //destination.position = nearestPatrolPoint.transform.position;
        //atDestination = false;
        agent = this.GetComponent<NavMeshAgent>();
        InvokeRepeating("CheckPostition", 0f, 0.5f);
        atDestination = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (inCombat == true)
        {
            if (isRetreating)
            {
                FleeBehavior();
            }
            CombatMovementBehavior();
        }
        if (isCivilian)
        {
            FleeBehavior();
        }
        else
        {
            PatrolMovementBehavior();
        }

    }
    void CheckPostition()
    {
        if (destination == null)
        {
            return;
        }
        distance = Vector3.Distance(destination.position, transform.position);
        if (distance <= 1)
        {
            atDestination = true;
        }
    }
    void CombatMovementBehavior()
    {
        if (target == null)
        {
            inCombat = false;
            return;
        }
        distance = Vector3.Distance(target.position, transform.position);
        if (distance <= 10)
        {
            destination = transform;
            agent.SetDestination(destination.position);
        }
        if (distance >= 10)
        {
            agent.SetDestination(target.position);
        }
        direction = target.position - transform.position;
        Quaternion FindTargetRotation = Quaternion.LookRotation(direction);
        rotation = Quaternion.Lerp(baseRotation.rotation, FindTargetRotation, Time.deltaTime * RotationSpeed).eulerAngles;
        baseRotation.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void PatrolMovementBehavior()
    {
        if (target == null && atDestination == true)
        {
            agent.ResetPath();
            nearestPartolNumber = Random.Range(0, patrolPathsArray.Length);
            nearestPatrolPoint = patrolPathsArray[nearestPartolNumber];
            agent.SetDestination(nearestPatrolPoint.position);
            destination = nearestPatrolPoint;
            atDestination = false;
        }
        else
        {
            return;
        }
    }
}
