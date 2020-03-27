using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    //Jeff
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask layerWalls;
    public LayerMask layerPlayer;

    public List<Transform> visableTargets = new List<Transform>();

    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", 0.2f);
    }
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisableTargets();
        }
    }
    void FindVisableTargets()
    {
        visableTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, layerPlayer);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, layerWalls))
                {
                    visableTargets.Add(target);
                }
            }
        }
    }

    public Vector3 DirectionFromAngle(float angeleDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angeleDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angeleDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angeleDegrees * Mathf.Deg2Rad));
    }
}
