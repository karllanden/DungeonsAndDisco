using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : AiProcessing
{
    //Jeff Code
    [Header("Set Patrol points, Can be 0")]
    [SerializeField] private Transform[] patrolPathsArray;
   
    [Header("Set Combat distance, Can be 0")]
    [SerializeField] private float combatDistance;
    
    [Header("Set body to be the center of rotation")]
    [SerializeField] private Transform baseRotation;

    [Header("Set Flee point")]
    [SerializeField] private Transform fleePoint;

    private Transform lastKnownLocation;
    private Transform nearestPatrolPoint;
    private int nearestPartolNumber;
    private bool isHunting, atDestination;

    NavMeshAgent agent;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        InvokeRepeating("CheckPostition", 0f, 0.5f);
        InvokeRepeating("TargetPostition", 0f, 0.1f);
        atDestination = true;
    }

    void Update()
    {
        if (isHunting == true)
        {
            HuntBehavior();
        }
        else
        {
            if (inCombat == true)
            {
                if (isRetreating || isCivilian)
                {
                    FleeBehavior();
                    FleeMovement();
                }
                else
                {
                    CombatMovementBehavior();
                }
            }

            if (isRetreating && isCivilian)
            {
                FleeBehavior();
                FleeMovement();
            }

            //if (isCivilian == true)
            //{
            //    //Dance
            //}
            else
            {
                PatrolMovementBehavior();
            }
        }
    }
    void TargetPostition()
    {
        if (target != null)
        {
            lastKnownLocation = target;
        }
    }
    void CheckPostition()
    {
        if (destination == null)
        {
            return;
        }
        distance = Vector3.Distance(destination.position, transform.position);
        if (distance <= 2)
        {
            atDestination = true;
            if (isHunting == true)
            {
                isHunting = false;
            }
        }
    }
    void FleeMovement()
    {
        agent.SetDestination(fleePoint.position);
    }

    void CombatMovementBehavior()
    {
        if (target == null)
        {
            isHunting = true;
            inCombat = false;
            return;
        }
        distance = Vector3.Distance(target.position, transform.position);
        if (distance <= combatDistance)
        {
            destination = transform;
            agent.SetDestination(destination.position);
            lookAtTarget();
        }
        if (distance >= combatDistance)
        {
            agent.SetDestination(target.position);
            lookAtTarget();
        }
        
    }
    void lookAtTarget()
    {
        direction = target.position - transform.position;
        Quaternion FindTargetRotation = Quaternion.LookRotation(direction);
        rotation = Quaternion.Lerp(baseRotation.rotation, FindTargetRotation, Time.deltaTime * RotationSpeed).eulerAngles;
        baseRotation.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void PatrolMovementBehavior()
    {
        if (patrolPathsArray.Length <= 0)
        {
            return;
        }
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
    void HuntBehavior()
    {
        if (target == null)
        {
            agent.SetDestination(lastKnownLocation.position);
            destination = lastKnownLocation;
        }
        if (target != null)
        {
            isHunting = false;
        }
    }
    private void BossMovement()
    {

    }
}
