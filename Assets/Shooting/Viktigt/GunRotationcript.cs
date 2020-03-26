using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotationcript : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //Vector3 distanceToPlayer = player.transform.position - this.transform.position;
        //if (distanceToPlayer.magnitude < 2)
        //{
        //    PickUp();
        //}
        GameObject target = GameObject.FindGameObjectWithTag("Target");
        transform.LookAt(target.transform - Quaternion.EulerAngles(90,0,0));
    }
}
