using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraSetting : MonoBehaviour
{
    public Camera cam;
    public Vector3 offset;
    public CameraMove cm;
    public Vector3 camAngle;


    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Floor")
        {
            cm.enabled = false;
            Debug.Log(collisionInfo.collider.tag);
            cam.transform.position = collisionInfo.transform.position + offset;
            cam.transform.rotation = Quaternion.Euler(camAngle);
        }
        else if (collisionInfo.collider.tag == "Untagged")
        {
            ;
        }
        else
        {
            Debug.Log(collisionInfo.collider.tag);
            cm.enabled = true;
        }
    }
}
