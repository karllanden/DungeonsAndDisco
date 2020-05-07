﻿using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : AiProcessing
{
    [SerializeField]
    private Transform[] patrolPathsArray;
    private Transform nearestPatrolPoint;
    private int nearestPartolNumber;
    private bool atDestination;

    [SerializeField]
    private Transform baseRotation;
    NavMeshAgent agent;

    [SerializeField]
    private Transform fleePoint;
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
            if (isRetreating || isCivilian)
            {
                FleeBehavior();
                FleeMovement();
            }
            CombatMovementBehavior();
        }

        if (isRetreating && isCivilian)
        {
            FleeBehavior();
            FleeMovement();
        }

        if (isCivilian == true)
        {
            //Dance
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
        if (distance <= 2)
        {
            atDestination = true;
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
}
