using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellingScript : MonoBehaviour
{

    bool playerHasEntered;
    [SerializeField] 
    private float fadePerSecond = 2.5f;

    [SerializeField]
    private GameObject ActiveObjecs1;
    [SerializeField]
    private GameObject ActiveObjecs2;

    void OnTriggerEnter(Collider other)
    {
        if (ActiveObjecs1 != null)
        {
            ActiveObjecs1.SetActive(true);
        }
        if (ActiveObjecs2 != null)
        {
            ActiveObjecs2.SetActive(true);
        }
        if (other.tag == "Player")
        {
            if (playerHasEntered == false)
            {
                gameObject.SetActive(false);
            }
        }


    }


    void Update()
    {
        var material = GetComponent<Renderer>().material;
        var color = material.color;

        material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));
    }
    void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (playerHasEntered != false)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
