using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] Transform gunPos;




    // Update is called once per frame
    void Update()
    {
        Vector3 aim = this.transform.position;
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        this.transform.position = ray.direction;
        this.transform.position = ray.direction - this.transform.position;
        if (Physics.Raycast(ray, out hit))
        {
            aim = hit.point;
        }
        aim.y = gunPos.position.y;
        this.transform.position = aim;
        

    }
}
